using AgileContent.Domain.FamilyNumber.Commands;
using AgileContent.Domain.FamilyNumber.Interface;
using Xunit;
using FluentAssertions;
using AgileContent.Domain.FamilyNumber.DTo;

namespace AgileContent.CrossCuttingTest.FamilyNumber.Commands
{
    public class FamilyNumberCommandsTest
    {
        private const string NegativeNumberResultErrorMessage = "Negative number informed";
        public ICalcFamilyNumberCommand CalcFamilyNumberCommand { get; }
        public FamilyNumberCommandsTest()
        {
            CalcFamilyNumberCommand = new CalcFamilyNumberCommand();
        }
        [Fact]
        public void NumberExceeds()
        {
            CalcFamilyNumberCommand.SetDto(new InputFamilyNumberDto() { Number = 100000001 });
            if (CalcFamilyNumberCommand.Valid().IsValid)
            {
                CalcFamilyNumberCommand.Execute();
                CalcFamilyNumberCommand.GetResult().Should().Be(-1, "Number not exceed max value");
            }
        }

        [Fact]
        public void NumberZero()
        {
            CalcFamilyNumberCommand.SetDto(new InputFamilyNumberDto() { Number = 0 });
            if (CalcFamilyNumberCommand.Valid().IsValid)
            {
                CalcFamilyNumberCommand.Execute();
                CalcFamilyNumberCommand.GetResult().Should().Be(0, "number not equals zero");
            }
        }

        [Theory]
        [InlineData(213, 321)]
        [InlineData(355, 553)]
        [InlineData(553, 553)]
        public void ExpectedResult(long number, int largestFamilyNumber)
        {
            CalcFamilyNumberCommand.SetDto(new InputFamilyNumberDto() { Number = number });
            if (CalcFamilyNumberCommand.Valid().IsValid)
            {
                CalcFamilyNumberCommand.Execute();
                CalcFamilyNumberCommand.GetResult().Should().Be(largestFamilyNumber, "Family number are not expected result.");
            }
        }

        [Theory]
        [InlineData(213, 388)]
        [InlineData(355, 878)]
        [InlineData(553, 849)]
        public void NotExpectedResult(long number, int largestFamilyNumber)
        {
            CalcFamilyNumberCommand.SetDto(new InputFamilyNumberDto() { Number = number });
            if (CalcFamilyNumberCommand.Valid().IsValid)
            {
                CalcFamilyNumberCommand.Execute();
                CalcFamilyNumberCommand.GetResult().Should().NotBe(largestFamilyNumber, "Family number are not expected result.");
            }
        }

        [Theory]
        [InlineData(-1, NegativeNumberResultErrorMessage)]
        [InlineData(-600, NegativeNumberResultErrorMessage)]
        [InlineData(-999999999999999999, NegativeNumberResultErrorMessage)]
        public void NegativeNumber(long number, string error)
        {
            CalcFamilyNumberCommand.SetDto(new InputFamilyNumberDto() { Number = number });
            var validResult = CalcFamilyNumberCommand.Valid();
            if (!validResult.IsValid)
            {
                validResult.Errors.Should().Contain(p => p.ErrorMessage == error, "Error not expected");
            }
        }
    }
}