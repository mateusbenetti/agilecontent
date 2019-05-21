using AgileContent.Application.Interface;
using AgileContent.Domain.FamilyNumber.Interface;

namespace AgileContent.Application.Service
{
    public class FamilyNumberService : IFamilyNumberService
    {
        private readonly ICalcFamilyNumberCommand _calcFamilyNumberCommand;
        public FamilyNumberService(ICalcFamilyNumberCommand calcFamilyNumberCommand)
        {
            _calcFamilyNumberCommand = calcFamilyNumberCommand;
        }
        int IFamilyNumberService.GetLargestFamilyNumber(long number)
        {
            _calcFamilyNumberCommand.SetNumber(number);
            _calcFamilyNumberCommand.Execute();
            return _calcFamilyNumberCommand.Result;
        }
    }
}