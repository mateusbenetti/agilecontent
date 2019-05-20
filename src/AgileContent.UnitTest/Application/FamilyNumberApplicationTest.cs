using System;
using AgileContent.Application.Interface;
using AgileContent.Application.Service;
using Xunit;
using FluentAssertions;

namespace AgileContent.CrossCuttingTest.Application
{
    public class FamilyNumberApplicationTest
    {
        private const string NegativeNumberResultErrorMessage = "Negative number informed";
        public IFamilyNumber FamilyNumbers { get; }
        public FamilyNumberApplicationTest()
        {
            FamilyNumbers = new FamilyNumber();
        }
        [Fact]
        public void NumberExceeds()
        {
          var  result = FamilyNumbers.GetLargestFamilyNumber(100000001);
          result.Should().Be(-1, "Number not exceed max value");
        }
        
        [Fact]
        public void NumberZero()
        {
            var result = FamilyNumbers.GetLargestFamilyNumber(0);
            result.Should().Be(0, "number not equals zero");
        }

        [Theory]
        [InlineData(213, 321)]
        [InlineData(355, 553)]
        [InlineData(553, 553)]
        public void ExpectedResult(long number, int largestFamilyNumber)
        {
            var result = FamilyNumbers.GetLargestFamilyNumber(number);
            result.Should().Be(largestFamilyNumber, "Family number are not expected result.");
        }

        [Theory]
        [InlineData(213, 388)]
        [InlineData(355, 878)]
        [InlineData(553, 849)]
        public void NotExpectedResult(long number, int largestFamilyNumber)
        {
            var result = FamilyNumbers.GetLargestFamilyNumber(number);
            result.Should().NotBe(largestFamilyNumber, "Family number are not expected result.");
        }

        [Theory]
        [InlineData(-1, NegativeNumberResultErrorMessage)]
        [InlineData(-600, NegativeNumberResultErrorMessage)]
        [InlineData(-999999999999999999, NegativeNumberResultErrorMessage)]
        public void NegativeNumber(long number, string error)
        {
            try
            {
                FamilyNumbers.GetLargestFamilyNumber(number);
            }
            catch (ApplicationException err)
            {
                err.Message.Should().Be(error, "Error not expected");
            }
        }
    }
}