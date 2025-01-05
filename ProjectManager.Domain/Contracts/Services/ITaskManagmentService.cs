using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Domain.Contracts.Services
{
    public interface ITaskManagmentService
    {
        Task UnassignTaskForUserInProject(string projectEncodedName, string userId);
        Task UnassignTaskForUser(string userId);
    }
}
