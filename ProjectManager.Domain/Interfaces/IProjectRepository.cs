using ProjectManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Domain.Interfaces
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
        Task<IEnumerable<ProjectUser>> GetProjectContributors(int projectId);
        Task AddContributorToProject(ProjectUser projectUser);
        Task RemoveContributor(ProjectUser projectUser);
        Task<bool> IsUserContributor(int projectId, string userId);
    }
}
