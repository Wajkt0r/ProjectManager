using ProjectManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Domain.Interfaces
{
    public interface IProjectTaskRepository
    {
        Task Commit();
        Task Create(ProjectTask projectTask);
        Task DeleteTask(ProjectTask projectTask);
        Task<IEnumerable<ProjectTask>> GetAllByEncodedName(string projectEncodedName);
        Task<IEnumerable<ProjectTask>> GetUserProjectTasks(string projectEncodedName, string userId);
        Task<ProjectTask> GetById(int id);
    }
}
