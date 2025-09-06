using FluentValidation;
using Buhoborec.Application.WorkLogs.Commands;

namespace Buhoborec.Application.WorkLogs.Validators;

public class LogWorkValidator : AbstractValidator<LogWorkCommand>
{
    public LogWorkValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Timestamp).NotEmpty();
    }
}
