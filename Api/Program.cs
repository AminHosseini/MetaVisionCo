using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using Api.Helpers;
using BaseLib.Application.Extensions;
//using BaseLib.Context.Interceptors;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.OData;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;
using Unchase.Swashbuckle.AspNetCore.Extensions.Options;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var edmModel = ServiceCollectionExtension.GetEdmModel();

builder.Services.AddControllers(configure =>
{
    configure.Filters.Add(
        new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
    configure.Filters.Add(
        new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
    configure.Filters.Add(
        new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
    configure.Filters.Add(
        new ProducesDefaultResponseTypeAttribute());

    configure.ReturnHttpNotAcceptable = true;
})
    .AddOData(opt => opt
        .OrderBy()
        .Filter()
        .Count()
        .Select()
        .SetMaxTop(250)
        .AddRouteComponents("api", edmModel))
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new RowVersionValueConverter());
            options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            //options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
            //options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.SwaggerDoc("v1", new OpenApiInfo { Title = builder.Environment.ApplicationName, Version = "v1" });
    setupAction.IgnoreObsoleteActions();
    setupAction.IgnoreObsoleteProperties();
    setupAction.DescribeAllParametersInCamelCase();
    var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
    xmlFiles.ForEach(xmlFile => setupAction.IncludeXmlComments(xmlFile));

    setupAction.AddEnumsWithValuesFixFilters(o =>
    {
        o.ApplySchemaFilter = true;

        o.XEnumNamesAlias = "x-enum-varnames";

        o.XEnumDescriptionsAlias = "x-enum-descriptions";

        o.ApplyParameterFilter = true;

        o.ApplyDocumentFilter = true;

        o.IncludeDescriptions = true;

        o.IncludeXEnumRemarks = true;

        o.DescriptionSource = DescriptionSources.DescriptionAttributesThenXmlComments;

        o.NewLine = "\n";

        xmlFiles.ForEach(xmlFile => o.IncludeXmlCommentsFrom(xmlFile));
    });

    setupAction.OperationFilter<SwaggerCheckOperationFilter>();
});

builder.Services.AddFluentValidationRulesToSwagger();

builder.Services.AddProblemDetails();

builder.Services.AddHttpContextAccessor();

//builder.Services.TryAddSingleton<MetaDataInterceptor>();

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<ApplicationDbContext>((serviceProvider, optionsBuilder) =>
{
    optionsBuilder.UseSqlServer(connectionString);
    //.AddInterceptors(serviceProvider.GetRequiredService<MetaDataInterceptor>());
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
    });
});

builder.Services.AddMapsterConfigurationsServices();

builder.Services.AddApplicationServices();

var app = builder.Build();

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

app.UseCors();

app.Run();
