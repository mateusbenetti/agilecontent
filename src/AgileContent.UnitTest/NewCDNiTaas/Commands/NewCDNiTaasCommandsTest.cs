using AgileContent.Domain.NewCDNiTaas.Commands;
using AgileContent.Domain.NewCDNiTaas.Interface;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AgileContent.CrossCuttingTest.NewCDNiTaas.Commands
{
    public class NewCDNiTaasCommandsTest
    {
        readonly IConvertCdnToNowLogFileCommand _convertCdnToNowLogFileCommand;
        readonly IReadFileContentCommand _readFileContentCommand;
        public NewCDNiTaasCommandsTest()
        {
            _convertCdnToNowLogFileCommand = new ConvertCdnToNowLogFileCommand();
            _readFileContentCommand = new ReadFileContentCommand();
        }
        
        [Theory]
        [InlineData("", "Url is Empty")]
        [InlineData("oeioiowieo", "Invalid Url")]
        [InlineData("https://fluentvalidation.net", "Invalid Extension")]
        public void InvalidUrls(string url, string errorMessage)
        {
            _readFileContentCommand.SetDto(new Domain.NewCDNiTaas.DTo.LogFileDTo() { Url = url });
            _readFileContentCommand.Valid().Errors.Should().Contain(p => p.ErrorMessage == errorMessage, "Url Validate is Broken");
        }

        [Fact]
        public void OnlyInvalidExtension()
        {
            _readFileContentCommand.SetDto(new Domain.NewCDNiTaas.DTo.LogFileDTo() { Url = "https://fluentvalidation.net" });
            _readFileContentCommand.Valid().Errors.Should().ContainSingle(p => p.ErrorMessage == "Invalid Extension", "Url Validate is Broken");
        }

        [Fact]
        public void ValidUrl()
        {
            _readFileContentCommand.SetDto(new Domain.NewCDNiTaas.DTo.LogFileDTo() { Url = "https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-01.txt" });
            _readFileContentCommand.Valid().IsValid.Should().BeTrue("Url Validate is Broken");
        }

    }
}
