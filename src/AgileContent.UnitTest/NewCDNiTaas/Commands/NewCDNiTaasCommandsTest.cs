using AgileContent.Domain.NewCDNiTaas.Commands;
using AgileContent.Domain.NewCDNiTaas.Interface;
using FluentAssertions;
using System.Collections.Generic;
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

        IList<string> ValidFormat
        {
            get
            {
                return new List<string>
                {
                    "312|200|HIT|\"GET /robots.txt HTTP/1.1\"|100.2",
                    "101|200|MISS|\"POST /myImages HTTP/1.1\"|319.4",
                    "199|404|MISS|\"GET /not-found HTTP/1.1\"|142.9",
                    "312|200|INVALIDATE|\"GET /robots.txt HTTP/1.1\"|245.1"
                };
            }
        }

        IList<string> InvalidFormat
        {
            get
            {
                return new List<string>
                {
                    "312|200|HIT|\"GET /roboçts.txt HTTP/1.1\"|100.2",
                    "101|200|MISS|\"PAST /myImages HTTP/1.1\"|319.4",
                    "AAA|404|MISS|\"GET /not-found HTTP/1.1\"|142.9",
                    "312|200|INVALIDATE|\"GET /robots.txt HTETP/1.1\"|245.1"
                };
            }
        }

        [Theory]
        [InlineData("", "Url is Empty")]
        [InlineData("oeioiowieo", "Invalid Url")]
        [InlineData("https://flueaaaaantvalidation.net", "Not Access Url")]
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
        public void ValidUrlAddress()
        {
            _readFileContentCommand.SetDto(new Domain.NewCDNiTaas.DTo.LogFileDTo() { Url = "https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-01.txt" });
            _readFileContentCommand.Valid().IsValid.Should().BeTrue("Url Validate is Broken");
        }

        [Fact]
        public void EmptyContent()
        {
            _convertCdnToNowLogFileCommand.SetDto(new Domain.NewCDNiTaas.DTo.LogFileDTo() { FileLines = new List<string>() });
            _convertCdnToNowLogFileCommand.Valid().Errors.Should().Contain(p => p.ErrorMessage == "Empty File Content", "Empty Content Validate Is Bronken!");
        }

        [Fact]
        public void IsValidContent()
        {
            _convertCdnToNowLogFileCommand.SetDto(new Domain.NewCDNiTaas.DTo.LogFileDTo() { FileLines = ValidFormat });
            _convertCdnToNowLogFileCommand.Valid().IsValid.Should().BeTrue("Valid Content Validate Is Bronken!");
        }

        [Fact]
        public void IsInvalidContent()
        {
            _convertCdnToNowLogFileCommand.SetDto(new Domain.NewCDNiTaas.DTo.LogFileDTo() { FileLines = InvalidFormat });
            _convertCdnToNowLogFileCommand.Valid().IsValid.Should().BeFalse("Invalid Content Validate Is Bronken!");
        }
    }
}
