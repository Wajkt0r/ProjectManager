using FluentValidation;
using ProjectManager.Domain.Interfaces;
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
                    .WithMessage("Ustaw nazwę projektu")
                .MinimumLength(5).WithMessage("Nazwa projektu musi miec minimum 5 znakow")
                .MaximumLength(50).WithMessage("Nazwa projektu nie moze przekraczac 50 znakow")
                .Custom((value, context) =>
                {
                    var existingProject = projectRepository.GetByName(value).Result;

                    if (existingProject != null)
                    {
                        context.AddFailure($"Nazwa '{value}' nie jest uniknalna nazwa projektu, uzyj innej nazwy");
                    }
                });

            RuleFor(p => p.Description)
                .NotEmpty()
                .MinimumLength(10).WithMessage("Prosze uzupelnic opis projektu, musi on zawierac minimum 10 znakow");

            RuleFor(p => p.FinishDate)
                .NotEmpty().WithMessage("Ustaw date koncowa projektu, mozesz ja przesunac pozniej")
                .Must(DateIsLaterThanNow).WithMessage("Data koncowa projektu nie moze być wczesniejsza niz aktualna");

        }

        private bool DateIsLaterThanNow(DateTime finishDate)
        {
            return finishDate > DateTime.UtcNow;
        }
    }
}
