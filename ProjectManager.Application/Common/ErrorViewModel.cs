using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Common
{
    public class ErrorViewModel
    {
        public string Title { get; set; } = default!;
        public string Message { get; set; } = default!;
        public string ActionName { get; set; } = default!;
        public string ControllerName { get; set; } = default!; 
    }
}
