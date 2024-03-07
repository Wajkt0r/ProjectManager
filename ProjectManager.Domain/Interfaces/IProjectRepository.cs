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
        Task<Project> GetByEncodedName(string encodedName);
        Task<Project> GetByName(string name);
    }
}
