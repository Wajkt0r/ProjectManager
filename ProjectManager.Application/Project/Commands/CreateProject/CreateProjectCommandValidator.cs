using FluentValidation;
using ProjectManager.Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Project.Commands.CreateProject
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator(IProjectRepository projectRepository)
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                    .WithMessage("Set project name")
                .MinimumLength(5).WithMessage("The project name must has at least 5 characters")
                .MaximumLength(50).WithMessage("The project name cannot exceed 50 characters")
                .Custom((value, context) =>
                {
                    var existingProject = projectRepository.GetByName(value).Result;

                    if (existingProject != null)
                    {
                        context.AddFailure($"Name '{value}' is not unique project name, try to use different name");
                    }
                });

            RuleFor(p => p.Description)
                .NotEmpty()
                .MinimumLength(10).WithMessage("Enter project description, must has at least 10 characters");

            RuleFor(p => p.FinishDate)
                .NotEmpty().WithMessage("Enter finish date, you can change it later")
                .Must(DateIsLaterThanNow).WithMessage("Finish date cannot be earlier than the today's date");

        }

        private bool DateIsLaterThanNow(DateTime finishDate)
        {
            return finishDate > DateTime.UtcNow;
        }
    }
}
