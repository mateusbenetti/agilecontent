using System.Linq;
using AgileContent.Domain.FamilyNumber.DTo;
using AgileContent.Domain.Behaviors;
using AgileContent.Domain.FamilyNumber.Validators;
using AgileContent.Domain.FamilyNumber.Interface;

namespace AgileContent.Domain.FamilyNumber.Commands
{
    public class CalcFamilyNumberCommand : CommandHandler<InputFamilyNumberDto, InputFamilyNumberDtoValidator, int>, ICalcFamilyNumberCommand
    {
        public override void Execute()
        {
            const int maxLargestFamilyNumberInput = 100000000;
            if (Dto.Number > default(int))
            {
                if (Dto.Number <= maxLargestFamilyNumberInput)
                {
                    var largestFamilyNumberString = string.Empty;
                    var stringNumber = Dto.Number.ToString();
                    var numbersChar = stringNumber.ToCharArray().Select(p => int.Parse(p.ToString()));
                    var orderNumberChar = numbersChar.OrderByDescending(p => p);
                    orderNumberChar.ToList().ForEach(p => largestFamilyNumberString += p.ToString());
                    Result = int.Parse(largestFamilyNumberString);
                }
                else
                {
                    Result = -1;
                }
            }
        }
    }
}