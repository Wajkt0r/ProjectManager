using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using ProjectManager.MVC.Models;

namespace ProjectManager.MVC.Extensions
{
    public static class ControllerExtensions
    {
        public static void SetNotification(this Controller controller, string type, string message)
        {
            var notification = new Notification(type, message);
            controller.TempData["Notification"] = JsonConvert.SerializeObject(notification);
        }
    }
}
