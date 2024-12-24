using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProjectManager.Domain.Entities;

namespace ProjectManager.Application.ProjectContributors.Commands.RemoveContributor
{
    public class RemoveContributorCommand : IRequest
    {
        public string ProjectEncodedName { get; set; } = default!;
        public string UserEmail { get; set; } = default!;
    }
}
