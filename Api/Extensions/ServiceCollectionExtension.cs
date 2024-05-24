using Api.Features.Pictures;
using Api.Features.Shop.ProductCategories;
using FluentValidation.AspNetCore;
using MapsterMapper;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using System.Reflection;

namespace Api.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        var config = TypeAdapterConfig.GlobalSettings;
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
        return services;
    }

    public static IServiceCollection AddMapsterConfigurationsServices(this IServiceCollection services)
    {
        ProductCategoryMapConfigs.RegisterMappingConfigurations(services);
        PictureMapConfigs.RegisterMappingConfigurations(services);

        return services;
    }

    public static IEdmModel GetEdmModel()
    {
        var builder = new ODataConventionModelBuilder();
        builder.EntitySet<ProductCategory>("ProductCategory")
            .EntityType
            .Filter()
            .Count()
            .OrderBy()
            .Select()
            .Page();

        return builder.GetEdmModel();
    }
}