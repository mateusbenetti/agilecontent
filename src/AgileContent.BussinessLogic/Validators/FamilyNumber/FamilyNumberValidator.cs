using FluentValidation;

namespace AgileContent.BussinessLogic.Validators
{
    public class FamilyNumberValidator : AbstractValidator<long>
    {
        public FamilyNumberValidator()
        {
            RuleFor(p => p).GreaterThan(-1).WithMessage("Negative number informed");
        }
    }
}
