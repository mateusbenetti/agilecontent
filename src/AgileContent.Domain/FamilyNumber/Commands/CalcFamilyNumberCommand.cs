using System;
using System.Linq;
using AgileContent.Domain.FamilyNumber.Interface;

namespace AgileContent.Domain.FamilyNumber.Commands
{
    public class CalcFamilyNumberCommand : ICalcFamilyNumberCommand
    {
        public CalcFamilyNumberCommand()
        {
            Result = 0;
        }
        public long Number { get; private set; }
        public int Result { get; private set; }
        public void Execute()
        {
            const int maxLargestFamilyNumberInput = 100000000;
            if (Number > default(int))
            {
                if (Number <= maxLargestFamilyNumberInput)
                {
                    var largestFamilyNumberString = string.Empty;
                    var stringNumber = Number.ToString();
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
            else if (Number < default(int))
            {
                throw new ApplicationException($"Negative number informed");
            }
        }
        public void SetNumber(long number)
        {
            Number = number;
        }
    }
}
