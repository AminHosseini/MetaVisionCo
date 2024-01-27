﻿using Api.Features.ProductCategories;
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

        return services;
    }

    //public static IEdmModel GetEdmModel()
    //{
    //    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    //    builder.EntitySet<Personnel>("Personnel")
    //        .EntityType
    //        .Filter()
    //        .Count()
    //        //.Expand()
    //        .OrderBy()
    //        .Page()
    //        .Select();

    //    return builder.GetEdmModel();
    //}
}