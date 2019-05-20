using System;
using System.Linq;
using AgileContent.Application.Interface;

namespace AgileContent.Application.Service
{
    public class FamilyNumber : IFamilyNumber
    {
        int IFamilyNumber.GetLargestFamilyNumber(long number)
        {
            const int maxLargestFamilyNumberInput = 100000000;
            var largestFamilyNumber = 0;
            if (number > default(int))
            {
                if (number <= maxLargestFamilyNumberInput)
                {
                    var largestFamilyNumberString = string.Empty;
                    var stringNumber = number.ToString();
                    var numbersChar = stringNumber.ToCharArray().Select(p => int.Parse(p.ToString()));
                    var orderNumberChar = numbersChar.OrderByDescending(p => p);
                    orderNumberChar.ToList().ForEach(p => largestFamilyNumberString += p.ToString());
                    largestFamilyNumber = int.Parse(largestFamilyNumberString);
                }
                else
                {
                    largestFamilyNumber = -1;
                }
            }
            else if (number < default(int))
            {
                throw new ApplicationException("Negative number informed");
            }

            return largestFamilyNumber;
        }
    }
}
