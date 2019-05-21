using AgileContent.Domain.Behaviors.Interface;
using AgileContent.Domain.FamilyNumber.DTo;
using AgileContent.Domain.FamilyNumber.Validators;

namespace AgileContent.Domain.FamilyNumber.Interface
{
    public interface ICalcFamilyNumberCommand : ICommandHandler<InputFamilyNumberDto, InputFamilyNumberDtoValidator, int>
    {
    }
}