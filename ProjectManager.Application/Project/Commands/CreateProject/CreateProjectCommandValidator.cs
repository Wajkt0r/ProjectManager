using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Project.Commands.CreateProject
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                    .WithMessage("Ustaw nazwę projektu")
                .MinimumLength(5).WithMessage("Nazwa projektu musi miec minimum 5 znakow")
                .MaximumLength(50).WithMessage("Nazwa projektu nie moze przekraczac 50 znakow");

            RuleFor(p => p.Description)
                .NotEmpty()
                .MinimumLength(10).WithMessage("Prosze uzupelnic opis projektu, musi on zawierac minimum 10 znakow");

            RuleFor(p => p.FinishDate)
                .NotEmpty().WithMessage("Ustaw datę końcową projektu, możesz ją przesunąć później")
                .Must(DateIsLaterThanNow).WithMessage("Data końcowa projektu nie może być wcześniejsza niż aktualna");

        }

        private bool DateIsLaterThanNow(DateTime finishDate)
        {
            return finishDate > DateTime.UtcNow;
        }
    }
}
