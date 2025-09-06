using FluentValidation;
using Buhoborec.Application.Tasks.Commands;
using System;

namespace Buhoborec.Application.Tasks.Validators;

public class UpdateTaskValidator : AbstractValidator<UpdateTaskCommand>
{
    public UpdateTaskValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Status).NotEmpty();
    }
}
