using System;
using AgileContent.Domain.FamilyNumber.Commands;
using AgileContent.Domain.FamilyNumber.Interface;
using Xunit;
using FluentAssertions;

namespace AgileContent.CrossCuttingTest.FamilyNumber.Command
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
            CalcFamilyNumberCommand.SetNumber(100000001);
            CalcFamilyNumberCommand.Execute();
            CalcFamilyNumberCommand.Result.Should().Be(-1, "Number not exceed max value");
        }
        
        [Fact]
        public void NumberZero()
        {
            CalcFamilyNumberCommand.SetNumber(0);
            CalcFamilyNumberCommand.Execute();
            CalcFamilyNumberCommand.Result.Should().Be(0, "number not equals zero");
        }

        [Theory]
        [InlineData(213, 321)]
        [InlineData(355, 553)]
        [InlineData(553, 553)]
        public void ExpectedResult(long number, int largestFamilyNumber)
        {
            CalcFamilyNumberCommand.SetNumber(number);
            CalcFamilyNumberCommand.Execute();
            CalcFamilyNumberCommand.Result.Should().Be(largestFamilyNumber, "Family number are not expected result.");
        }

        [Theory]
        [InlineData(213, 388)]
        [InlineData(355, 878)]
        [InlineData(553, 849)]
        public void NotExpectedResult(long number, int largestFamilyNumber)
        {
            CalcFamilyNumberCommand.SetNumber(number);
            CalcFamilyNumberCommand.Execute();
            CalcFamilyNumberCommand.Result.Should().NotBe(largestFamilyNumber, "Family number are not expected result.");
        }

        [Theory]
        [InlineData(-1, NegativeNumberResultErrorMessage)]
        [InlineData(-600, NegativeNumberResultErrorMessage)]
        [InlineData(-999999999999999999, NegativeNumberResultErrorMessage)]
        public void NegativeNumber(long number, string error)
        {
            try
            {
                CalcFamilyNumberCommand.SetNumber(number);
                CalcFamilyNumberCommand.Execute();
            }
            catch (ApplicationException err)
            {
                err.Message.Should().Be(error, "Error not expected");
            }
        }
    }
}