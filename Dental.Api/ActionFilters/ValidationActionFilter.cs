using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Dental.Domain.Models.Response;

namespace Dental.Api.ActionFilters;


[AttributeUsage(AttributeTargets.Method)]
public class ValidationActionFilter : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context,
    ActionExecutionDelegate next)
    {
        if (context.ModelState.IsValid)
        {
            await next();
        }
        else
        {
            var errors = context.ModelState
                .SelectMany(ms => ms.Value.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            var errorResponse = new
            {
                Status = false,
                Errors = errors.FirstOrDefault(),
                Data = "",
                Global = true,
            };

            ResponseModel.Create(errorResponse, false, false, "");
            context.Result = new BadRequestObjectResult(errorResponse);
        }
    }
}
