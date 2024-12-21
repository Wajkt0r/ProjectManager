﻿using AutoMapper;
using MediatR;
using ProjectManager.Application.ApplicationUser;
using ProjectManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Project.Commands.CreateProject
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public CreateProjectCommandHandler(IProjectRepository projectRepository, IMapper mapper, IUserContext userContext)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _userContext = userContext;
        }
        public async Task<Unit> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();

            if (currentUser == null || (!currentUser.IsInRole("User") && !currentUser.IsInRole("Admin")))
            {
                return Unit.Value;
            }

            var project = _mapper.Map<Domain.Entities.Project>(request);
            project.CreatedById = currentUser.Id;
            project.EncodeName();

            await _projectRepository.Create(project);
            return Unit.Value;
        }
    }
}
