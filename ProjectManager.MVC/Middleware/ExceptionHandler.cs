using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using ProjectManager.Application.Common;
using ProjectManager.Application.Common.Exceptions;
using ProjectManager.MVC.Models;

namespace ProjectManager.MVC.Middleware
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                await HandleExceptionAsync(context, ex, "Not Found", ex.Message);
            }
            catch (ForbiddenAccessException ex)
            {
                await HandleExceptionAsync(context, ex, "Forbidden access", ex.Message);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, "Server problem", ex.Message);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex, string title, string message)
        {
            var errorViewModel = new ProjectManager.Application.Common.ErrorViewModel
            {
                Title = title,
                Message = message,
                ActionName = "Project",
                ControllerName = "Index"
            };

            var queryString = $"?title={Uri.EscapeDataString(errorViewModel.Title)}" +
                              $"&message={Uri.EscapeDataString(errorViewModel.Message)}" +
                              $"&actionName={Uri.EscapeDataString(errorViewModel.ActionName)}" +
                              $"&controllerName={Uri.EscapeDataString(errorViewModel.ControllerName)}";

            context.Response.Redirect($"/Error/HandleError{queryString}");
            return Task.CompletedTask;  
        }
    }
}
