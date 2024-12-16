using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProjectManager.Domain.Entities;

namespace ProjectManager.Application.Project.Commands.RemoveContributor
{
    public class RemoveContributorCommand : IRequest
    {
        public ProjectUser ProjectUser { get; set; }
    }
}
