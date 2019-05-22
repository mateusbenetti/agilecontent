using AgileContent.Domain.NewCDNiTaas.Commands;
using AgileContent.Domain.NewCDNiTaas.DTo;
using AgileContent.Domain.NewCDNiTaas.Interface;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace AgileContent.CrossCuttingTest.NewCDNiTaas.Commands
{
    public class NewCDNiTaasCommandsTest
    {
        readonly IConvertCdnToNowLogFileCommand _convertCdnToNowLogFileCommand;
        readonly IReadFileContentCommand _readFileContentCommand;
        readonly ICreateNowLogFileContentCommand _createNowLogFileContentCommand;
        public NewCDNiTaasCommandsTest()
        {
            _convertCdnToNowLogFileCommand = new ConvertCdnToNowLogFileCommand();
            _readFileContentCommand = new ReadFileContentCommand();
            _createNowLogFileContentCommand = new CreateNowLogFileContentCommand();
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


        string ValidOutPutContent(string version, DateTime dateTime)
        {
            return string.Format(@"#Version: {0}
#Date: {1} {2}
#Fields: provider http-method status-code uri-path time-taken response-size cache-status
""Minha CDN"" GET 200 /robots.txt 100 312 HIT
""Minha CDN"" POST 200 /myImages 319 101 MISS
""Minha CDN"" GET 404 /not-found 142 199 MISS
""Minha CDN"" GET 200 /robots.txt 245 312 REFRESH_HIT", version, dateTime.ToShortDateString(), dateTime.ToLongTimeString()).Trim();
        }

        [Theory]
        [InlineData("", "Url is Empty")]
        [InlineData("oeioiowieo", "Invalid Url")]
        [InlineData("https://flueaaaaantvalidation.net", "Not Access Url")]
        [InlineData("https://fluentvalidation.net", "Invalid Extension")]
        public void InvalidUrls(string url, string errorMessage)
        {
            _readFileContentCommand.SetDto(new LogFileDTo() { Url = url });
            _readFileContentCommand.Valid().Errors.Should().Contain(p => p.ErrorMessage == errorMessage, "Url Validate is Broken");
        }

        [Fact]
        public void OnlyInvalidExtension()
        {
            _readFileContentCommand.SetDto(new LogFileDTo() { Url = "https://fluentvalidation.net" });
            _readFileContentCommand.Valid().Errors.Should().ContainSingle(p => p.ErrorMessage == "Invalid Extension", "Url Validate is Broken");
        }

        [Fact]
        public void ValidUrlAddress()
        {
            _readFileContentCommand.SetDto(new LogFileDTo() { Url = "https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-01.txt" });
            _readFileContentCommand.Valid().IsValid.Should().BeTrue("Url Validate is Broken");
        }

        [Fact]
        public void EmptyContent()
        {
            _convertCdnToNowLogFileCommand.SetDto(new LogFileDTo() { FileLines = new List<string>() });
            _convertCdnToNowLogFileCommand.Valid().Errors.Should().Contain(p => p.ErrorMessage == "Empty File Content", "Empty Content Validate Is Bronken!");
        }

        [Fact]
        public void IsValidContent()
        {
            _convertCdnToNowLogFileCommand.SetDto(new LogFileDTo() { FileLines = ValidFormat });
            _convertCdnToNowLogFileCommand.Valid().IsValid.Should().BeTrue("Valid Content Validate Is Bronken!");
        }

        [Fact]
        public void IsInvalidContent()
        {
            _convertCdnToNowLogFileCommand.SetDto(new LogFileDTo() { FileLines = InvalidFormat });
            _convertCdnToNowLogFileCommand.Valid().IsValid.Should().BeFalse("Invalid Content Validate Is Bronken!");
        }
        [Fact]
        public void ThereIsNotEvents()
        {
            _convertCdnToNowLogFileCommand.SetDto(new LogFileDTo() { FileLines = ValidFormat });
            _convertCdnToNowLogFileCommand.Execute();
            var nowLogFileModel = _convertCdnToNowLogFileCommand.GetResult();
            nowLogFileModel.Events.Clear();
            _createNowLogFileContentCommand.SetDto(new LogFileDTo() { NowLogFileModel = nowLogFileModel });
            _createNowLogFileContentCommand.Valid().Errors.Should().Contain(p => p.ErrorMessage == "There is not events for build now log file.", "There Is Not Events Validate is Bronken!");
        }

        [Fact]
        public void ThereIsEvents()
        {
            _convertCdnToNowLogFileCommand.SetDto(new LogFileDTo() { FileLines = ValidFormat });
            _convertCdnToNowLogFileCommand.Execute();
            var nowLogFileModel = _convertCdnToNowLogFileCommand.GetResult();
            _createNowLogFileContentCommand.SetDto(new LogFileDTo() { NowLogFileModel = nowLogFileModel });
            _createNowLogFileContentCommand.Valid().IsValid.Should().BeTrue("There Is Events Validate is Bronken!");
        }

        [Fact]
        public void ValidOutContent()
        {
            var dateTime = DateTime.Now;
            var version = "1.0";
            var dto = new LogFileDTo() { FileLines = ValidFormat, DateTime = dateTime, Version = version };
            _convertCdnToNowLogFileCommand.SetDto(dto);
            _convertCdnToNowLogFileCommand.Execute();
            dto.NowLogFileModel = _convertCdnToNowLogFileCommand.GetResult();

            _createNowLogFileContentCommand.SetDto(dto);
            _createNowLogFileContentCommand.Execute();
            var valid = _createNowLogFileContentCommand.GetResult() == ValidOutPutContent(version, dateTime);
            valid.Should().BeTrue("Not Work Convert File");
        }

        [Fact]
        public void InvalidOutContent()
        {
            var dateTime = DateTime.Now;
            var version = "1.0";
            var dto = new LogFileDTo() { FileLines = ValidFormat, DateTime = dateTime, Version = version };
            _convertCdnToNowLogFileCommand.SetDto(dto);
            _convertCdnToNowLogFileCommand.Execute();
            dto.NowLogFileModel = _convertCdnToNowLogFileCommand.GetResult();

            _createNowLogFileContentCommand.SetDto(dto);
            _createNowLogFileContentCommand.Execute();
            var valid = _createNowLogFileContentCommand.GetResult() == ValidOutPutContent(version, dateTime).Replace(" ", "  ");
            valid.Should().BeFalse("Not Work Invalid Out Content Validade");
        }
    }
}