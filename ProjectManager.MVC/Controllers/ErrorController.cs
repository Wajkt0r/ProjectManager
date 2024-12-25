using Microsoft.AspNetCore.Mvc;
using ProjectManager.Application.Common;

namespace ProjectManager.MVC.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/HandleError")]
        public ActionResult HandleError(string title, string message, string actionName, string controllerName)
        {
            var model = new ErrorViewModel
            {
                Title = title,
                Message = message,
                ActionName = actionName,
                ControllerName = controllerName
            };

            return View("Error", model);
        }
    }
}
