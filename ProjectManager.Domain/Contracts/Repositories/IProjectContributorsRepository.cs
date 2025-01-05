using ProjectManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Domain.Contracts.Repositories
{
    public interface IProjectContributorsRepository
    {
        Task<IEnumerable<ProjectUser>> GetProjectContributors(int projectId);
        Task AddContributorToProject(ProjectUser projectUser);
        Task RemoveContributor(ProjectUser projectUser);
        Task<bool> IsUserContributor(int projectId, string userId);
        Task<List<string>> GetUserProjectRoles(int projectId, string userId);
        Task<List<ProjectRole>> GetAvailableProjectRoles();
        Task AddUserProjectRoles(List<ProjectUserRole> projectUserRoles);
        Task RemoveUserProjectRoles(List<ProjectUserRole> projectUserRoles);
    }
}
