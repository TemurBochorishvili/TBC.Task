using Api.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.ActionFilters;

public class ValidationFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var str = "";
            foreach (var item in context.ModelState)
            {
                str += $"\n {item.Key}: ";
                foreach (var innerItems in item.Value.Errors)
                {
                    str += $"{innerItems.ErrorMessage} ";
                }
            }
            throw new AppException(str);
        }
    }
}
