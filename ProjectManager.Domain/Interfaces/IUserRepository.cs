using Microsoft.AspNetCore.Identity;
using ProjectManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task Commit();
        Task<IEnumerable<User>>GetAllUsers();
    }
}
