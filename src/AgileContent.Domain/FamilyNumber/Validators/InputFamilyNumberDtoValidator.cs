using AgileContent.Domain.FamilyNumber.DTo;
using FluentValidation;

namespace AgileContent.Domain.FamilyNumber.Validators
{
    public class InputFamilyNumberDtoValidator : AbstractValidator<InputFamilyNumberDto>
    {
        public InputFamilyNumberDtoValidator()
        {
            RuleFor(p => p.Number).GreaterThan(0).WithMessage("Negative number informed");
        }
    }
}
