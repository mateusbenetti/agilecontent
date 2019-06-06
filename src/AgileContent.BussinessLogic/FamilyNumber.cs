using AgileContent.BussinessLogic.Interface;
using AgileContent.BussinessLogic.Validators;

namespace AgileContent.BussinessLogic
{
    public class FamilyNumber : Behaviors.BussinessLogic, IFamilyNumber
    {
        readonly FamilyNumberValidator _familyNumberValidator;
        public FamilyNumber()
        {
            _familyNumberValidator = new FamilyNumberValidator();
        }
        int IFamilyNumber.GetLargestFamilyNumber(long number)
        {
            const int maxLargestFamilyNumberInput = 100000000;
            int largestFamilyNumber = 0;
            var valid = _familyNumberValidator.Validate(number);
            if (valid.IsValid)
            {
                if (number > default(int))
                    if (number <= maxLargestFamilyNumberInput)
                        largestFamilyNumber = CalcLargestFamilyNumber(number);
                   else
                        largestFamilyNumber = -1;
            }
            else
                Errors = valid.Errors;
            return largestFamilyNumber;
        }

        int CalcLargestFamilyNumber(long number)
        {
            int[] numbersOccurrences = new int[10];
            var largestFamilyNumberString = string.Empty;
            var stringNumber = number.ToString();
            foreach (char item in stringNumber)
                numbersOccurrences[int.Parse(item.ToString())]++;
            for (int x = 9; x >= 0; x--)
                for (int y = 0; y < numbersOccurrences[x]; y++)
                    largestFamilyNumberString += x;
            return int.Parse(largestFamilyNumberString);
        }
    }
}