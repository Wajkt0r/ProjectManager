using FluentValidation;
using ProjectManager.Application.ProjectTask.Commands.CreateProjectTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectTask.Commands.EditProjectTask
{
    public class EditProjectTaskCommandValidator : AbstractValidator<EditProjectTaskCommand>
    {
        public EditProjectTaskCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().NotNull()
                    .WithMessage("Enter task name");
            RuleFor(p => p.Description).NotEmpty().NotNull()
                .WithMessage("Enter task description");
            RuleFor(p => p.Deadline)
                .NotEmpty().NotNull().WithMessage("Enter task deadline date")
                .Must(DateIsLaterThanNow).WithMessage("Task deadline date cannot be earlier than today's date");

        }

        private bool DateIsLaterThanNow(DateTime deadLine)
        {
            return deadLine > DateTime.UtcNow;
        }

    }
}