using AgileContent.Application.Interface;
using AgileContent.Domain.FamilyNumber.DTo;
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
            _calcFamilyNumberCommand.SetDto(new InputFamilyNumberDto() { Number = number });
            if (_calcFamilyNumberCommand.Valid().IsValid)
            {
                _calcFamilyNumberCommand.Execute();
                return _calcFamilyNumberCommand.GetResult();
            }
            else
            {
                return 0;
            }            
        }
    }
}