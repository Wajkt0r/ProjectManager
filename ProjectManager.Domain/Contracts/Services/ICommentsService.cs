using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Domain.Contracts.Services
{
    public interface ICommentsService
    {
        Task DeleteUserComments(string? projectId, string userId);
    }
}
