using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using Api.Helpers;
using BaseLib.Application.Extensions;
using BaseLib.Context.Interceptors;

var builder = WebApplication.CreateBuilder(args);

//var edmModel = ServiceCollectionExtension.GetEdmModel();

builder.Services.AddControllers(/*configure =>*/
//{
//    configure.Filters.Add(
//        new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
//    configure.Filters.Add(
//        new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
//    configure.Filters.Add(
//        new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
//    configure.Filters.Add(
//        new ProducesDefaultResponseTypeAttribute());


//    configure.ReturnHttpNotAcceptable = true;
/*}*/)
    //.AddOData(opt => opt//.Select()
    //.OrderBy()
    //.Filter()
    //.Count()
    //// .Expand()
    //.SetMaxTop(250)
    //.AddRouteComponents("api", edmModel))
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new RowVersionValueConverter()));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.SwaggerDoc("v1", new OpenApiInfo { Title = builder.Environment.ApplicationName, Version = "v1" });
    setupAction.IgnoreObsoleteActions();
    setupAction.IgnoreObsoleteProperties();
    setupAction.DescribeAllParametersInCamelCase();
    var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
    xmlFiles.ForEach(xmlFile => setupAction.IncludeXmlComments(xmlFile));

    //setupAction.AddEnumsWithValuesFixFilters(o =>
    //{
    //    // add schema filter to fix enums (add 'x-enumNames' for NSwag or its alias from XEnumNamesAlias) in schema
    //    o.ApplySchemaFilter = true;

    //    // alias for replacing 'x-enumNames' in swagger document
    //    o.XEnumNamesAlias = "x-enum-varnames";

    //    // alias for replacing 'x-enumDescriptions' in swagger document
    //    o.XEnumDescriptionsAlias = "x-enum-descriptions";

    //    // add parameter filter to fix enums (add 'x-enumNames' for NSwag or its alias from XEnumNamesAlias) in schema parameters
    //    o.ApplyParameterFilter = true;

    //    // add document filter to fix enums displaying in swagger document
    //    o.ApplyDocumentFilter = true;

    //    // add descriptions from DescriptionAttribute or xml-comments to fix enums (add 'x-enumDescriptions' or its alias from XEnumDescriptionsAlias for schema extensions) for applied filters
    //    o.IncludeDescriptions = true;

    //    // add remarks for descriptions from xml-comments
    //    o.IncludeXEnumRemarks = true;

    //    // get descriptions from DescriptionAttribute then from xml-comments
    //    o.DescriptionSource = DescriptionSources.DescriptionAttributesThenXmlComments;

    //    // new line for enum values descriptions
    //    // o.NewLine = Environment.NewLine;
    //    o.NewLine = "\n";

    //    // get descriptions from xml-file comments on the specified path
    //    // should use "options.IncludeXmlComments(xmlFilePath);" before
    //    //o.IncludeXmlCommentsFrom(xmlFilePath);
    //    xmlFiles.ForEach(xmlFile => o.IncludeXmlCommentsFrom(xmlFile));
    //    // the same for another xml-files...
    //});
    //setupAction.OperationFilter<SwaggerCheckOperationFilter>();
});

//builder.Services.AddFluentValidationRulesToSwagger();

//builder.Services.AddProblemDetails();

//builder.Services.AddHttpContextAccessor();

builder.Services.TryAddSingleton<MetaDataInterceptor>();

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<ApplicationDbContext>((serviceProvider, optionsBuilder) =>
{
    optionsBuilder.UseSqlServer(connectionString)
    .AddInterceptors(serviceProvider.GetRequiredService<MetaDataInterceptor>());
});

builder.Services.AddMapsterConfigurationsServices();

builder.Services.AddApplicationServices();

var app = builder.Build();

//app.UseStatusCodePages();

//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();

app.UseSwaggerUI();

app.UseStatusCodePages();
//}
//else
//{
app.UseExceptionHandler(configure =>
{
    configure.Run(async context =>
    {
        context.Response.ContentType = "application/problem+json";
        if (context.RequestServices.GetService<IProblemDetailsService>() is { } problemDetailsService)
        {
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            var exception = exceptionHandlerPathFeature?.Error;
            var path = exceptionHandlerPathFeature?.Path;

            if (exception is not null)
            {
                if (exception is RecordNotFoundException)
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                else
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                string? uriProblemType = context.Response.StatusCode.GetUriProblemType();

                await problemDetailsService.WriteAsync(new ProblemDetailsContext
                {
                    HttpContext = context,
                    ProblemDetails =
                        {
                                Title = exception.GetType().Name,
                                Detail = exception.Message,
                                Type = uriProblemType,
                                Status = context.Response.StatusCode,
                                Instance = path
                        }
                });
            }
        }
    });
});
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
