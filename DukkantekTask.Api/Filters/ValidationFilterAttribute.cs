using DukkantekTask.Service.Models.Responses.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace DukkantekTask.Api.Filters
{
    public class ValidationFilterAttribute : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                // instead of directly returning the bad request model state highlighting the fields and its validation errors,
                // we create a new "Response<T>" to serve as unified application response,
                // we pass T object as BadRequestObjectResult and ModelState
                // by using SuppressModelStateInvalidFilter, we force the api services to not return it's own validation response
                // the "Value" object of "Response" will contains an array of invalid properties and it's validation error messages

                Log.Warning($"Invalid object request passed for method ({context.RouteData.Values["action"]}) in controller ({context.RouteData.Values["controller"]})");

                var response = new Response<object>
                {
                    IsSuccessful = false,
                    Message = "Request object passed is invalid",
                    Value = new BadRequestObjectResult(context.ModelState).Value
                };

                context.Result = new BadRequestObjectResult(response);
            }
        }
        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
