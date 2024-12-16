using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Common
{
    public class CommandResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public static CommandResult Success(string Message = "Success")
            => new CommandResult { IsSuccess = true, Message = Message, StatusCode = 200 };

        public static CommandResult Failure(string message, int statusCode = 400)
            => new CommandResult { IsSuccess = false, Message = message, StatusCode = statusCode };
    }
}
