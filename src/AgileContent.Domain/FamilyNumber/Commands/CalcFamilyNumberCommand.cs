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
                    int[] numbersOccurrences = new int[10];
                    var largestFamilyNumberString = string.Empty;
                    var stringNumber = Dto.Number.ToString();
                    foreach (char number in stringNumber)
                        numbersOccurrences[int.Parse(number.ToString())]++;
                    for (int x = 9; x >= 0; x--)
                        for (int y = 0; y < numbersOccurrences[x]; y++)
                            largestFamilyNumberString += x;

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