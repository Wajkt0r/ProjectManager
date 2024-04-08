using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectTask.Commands.CreateProjectTask
{
    public class CreateProjectTaskCommandValidator : AbstractValidator<CreateProjectTaskCommand>
    {
        public CreateProjectTaskCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().NotNull()
                .WithMessage("Podaj nazwe taska");
            RuleFor(p => p.Description).NotEmpty().NotNull()
                .WithMessage("Podaj opis taska");
            RuleFor(p => p.Deadline)
                .NotEmpty().NotNull().WithMessage("Ustaw deadline taska")
                .Must(DateIsLaterThanNow).WithMessage("Deadlina taska, musi byc pozniej niz teraz");

        }

        private bool DateIsLaterThanNow(DateTime deadLine)
        {
            return deadLine > DateTime.UtcNow;
        }
    }
}

