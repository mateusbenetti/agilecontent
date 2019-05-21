using AgileContent.Domain.Interface;

namespace AgileContent.Domain.FamilyNumber.Interface
{
    public interface ICalcFamilyNumberCommand : ICommandHandler
    {
        void SetNumber(long number);
        int Result { get;  }
    }
}