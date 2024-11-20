using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Query;

namespace Api.Filters;

/// <summary>
/// و اضافه کردن قابلیت شمارش و نمایش تعداد به آن Odata فعال کردن
/// </summary>
public sealed class EnableQueryWithMetadataAttribute : EnableQueryAttribute
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        base.OnActionExecuted(actionExecutedContext);

        if (actionExecutedContext?.Result is ObjectResult obj && obj.Value is IQueryable qry)
        {
            obj.Value = qry;
            long? count = actionExecutedContext.HttpContext.Request.ODataFeature().TotalCount;
            actionExecutedContext.HttpContext.Response.Headers.SetTotalCount(count);
        }

    }
}
