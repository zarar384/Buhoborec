using FluentValidation;
using Buhoborec.Application.Absences.Commands;
using System;

namespace Buhoborec.Application.Absences.Validators;

public class CreateAbsenceValidator : AbstractValidator<CreateAbsenceCommand>
{
    public CreateAbsenceValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.StartDate).LessThanOrEqualTo(x => x.EndDate);
    }
}
