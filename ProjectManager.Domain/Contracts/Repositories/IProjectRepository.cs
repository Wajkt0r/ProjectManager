using ProjectManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Domain.Contracts.Repositories
{
    public interface IProjectRepository
    {
        Task Commit();
        Task Create(Project project);
        Task<IEnumerable<Project>> GetAll();
        Task<string?> GetProjectEncodedNameByTaskId(int taskId);
        Task<Project> GetByEncodedName(string encodedName);
        Task<Project> GetByName(string name);
        Task DeleteProject(Project project);
        Task<int> GetProjectId(string encodedName);
        Task<List<Project>> GetAllUserProjects(string userId);
        Task DeleteAllUserProjects(string userId);
        Task<ProjectRole> GetProjectRole(int projectId, string roleName);
        Task<List<ProjectRole>> GetProjectRoles(int projectId);
        Task CreateProjectRole(ProjectRole projectRole);
        Task RemoveProjectRole(ProjectRole projectRole);
    }
}
