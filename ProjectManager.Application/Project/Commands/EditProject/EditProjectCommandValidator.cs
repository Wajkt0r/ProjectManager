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
