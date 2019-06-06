using AgileContent.BussinessLogic.Interface.Behaviors;

namespace AgileContent.BussinessLogic.Interface
{
    public interface IFamilyNumber : IBussinessLogic
    {
        int GetLargestFamilyNumber(long number);
    }
}
