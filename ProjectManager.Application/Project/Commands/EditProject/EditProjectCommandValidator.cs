using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Project.Commands.EditProject
{
    public class EditProjectCommandValidator : AbstractValidator<EditProjectCommand>
    {
        public EditProjectCommandValidator()
        {
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
