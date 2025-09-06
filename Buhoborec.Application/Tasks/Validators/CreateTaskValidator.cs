using FluentValidation;
using Buhoborec.Application.Tasks.Commands;

namespace Buhoborec.Application.Tasks.Validators;

public class CreateTaskValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
    }
}
