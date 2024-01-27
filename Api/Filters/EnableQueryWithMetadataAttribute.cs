using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Query;

namespace Api.Filters;

public sealed class EnableQueryWithMetadataAttribute : EnableQueryAttribute
{
    public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
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
