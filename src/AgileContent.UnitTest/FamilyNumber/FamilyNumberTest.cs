using Xunit;
using FluentAssertions;
using AgileContent.BussinessLogic.Interface;

namespace AgileContent.CrossCutting.Test.FamilyNumber
{
    public class FamilyNumberCommandsTest
    {
        private const string NegativeNumberResultErrorMessage = "Negative number informed";
        public IFamilyNumber FamilyNumber { get; }
        public FamilyNumberCommandsTest()
        {
            FamilyNumber = new BussinessLogic.FamilyNumber();
        }
        [Fact]
        public void NumberExceeds()
        {
            var result = FamilyNumber.GetLargestFamilyNumber(100000001);
            result.Should().Be(-1, "Number not exceed max value");
        }

        [Fact]
        public void NumberZero()
        {
            var result = FamilyNumber.GetLargestFamilyNumber(0);
            result.Should().Be(0, "number not equals zero");
        }

        [Theory]
        [InlineData(7090032, 9732000)]
        [InlineData(213, 321)]
        [InlineData(553, 553)]
        [InlineData(355, 553)]
        public void ExpectedResult(long number, int largestFamilyNumber)
        {
            var result = FamilyNumber.GetLargestFamilyNumber(number);
            result.Should().Be(largestFamilyNumber, "Family number are not expected result");
        }

        [Theory]
        [InlineData(213, 388)]
        [InlineData(355, 878)]
        [InlineData(553, 849)]
        public void NotExpectedResult(long number, int largestFamilyNumber)
        {
            var result = FamilyNumber.GetLargestFamilyNumber(number);
            result.Should().NotBe(largestFamilyNumber, "Family number are not expected result");
        }

        [Theory]
        [InlineData(-1, NegativeNumberResultErrorMessage)]
        [InlineData(-600, NegativeNumberResultErrorMessage)]
        [InlineData(-999999999999999999, NegativeNumberResultErrorMessage)]
        public void NegativeNumber(long number, string error)
        {
            var result = FamilyNumber.GetLargestFamilyNumber(number);
            FamilyNumber.Errors.Should().Contain(p => p.ErrorMessage == error, "Error not expected");
        }
    }
}