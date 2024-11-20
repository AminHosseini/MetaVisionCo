using Api.Features.Pictures;
using Api.Features.Shop.ProductCategories;
using Api.Features.Shop.Products;
using FluentValidation.AspNetCore;
using MapsterMapper;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using System.Reflection;

namespace Api.Extensions;

/// <summary>
/// کلاس کمکی سرویس ها
/// </summary>
public static class ServiceCollectionExtension
{
    /// <summary>
    /// سرویس های برنامه
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
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

    /// <summary>
    /// mapster سرویس های
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddMapsterConfigurationsServices(this IServiceCollection services)
    {
        ProductCategoryMapConfigs.RegisterMappingConfigurations(services);
        PictureMapConfigs.RegisterMappingConfigurations(services);
        ProductMapConfigs.RegisterMappingConfigurations(services);

        return services;
    }

    /// <summary>
    /// EDM مدل
    /// </summary>
    /// <returns></returns>
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