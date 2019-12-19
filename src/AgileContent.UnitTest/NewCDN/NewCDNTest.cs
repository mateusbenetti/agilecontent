using AgileContent.BussinessLogic;
using AgileContent.BussinessLogic.Interface;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace AgileContent.CrossCutting.Test.NewCDN
{
    public class NewCDNTest
    {
        readonly BussinessLogic.NewCDN _newCDN;
        public NewCDNTest()
        {
            _newCDN = new BussinessLogic.NewCDN();
        }

        string ValidOutPutContent(string version, DateTime dateTime)
        {
            return string.Format(@"#Version: {0}
#Date: {1} {2}
#Fields: provider http-method status-code uri-path time-taken response-size cache-status
""MINHA CDN"" GET 200 /robots.txt 100 312 HIT
""MINHA CDN"" POST 200 /myImages 319 101 MISS
""MINHA CDN"" GET 404 /not-found 143 199 MISS
""MINHA CDN"" GET 200 /robots.txt 245 312 REFRESH_HIT", version, dateTime.ToShortDateString(), dateTime.ToLongTimeString()).Trim();
        }

        string ValidHeader(string version, DateTime dateTime)
        {
            return string.Format(@"#Version: {0}
#Date: {1} {2}
#Fields: provider http-method status-code uri-path time-taken response-size cache-status
", version, dateTime.ToShortDateString(), dateTime.ToLongTimeString());
    }

        [Theory]
        [InlineData("", "Url is Empty")]
        [InlineData("oeioiowieo", "Invalid Url")]
        [InlineData("https://flueaaaaantvalidation.net", "Not Access Url")]
        [InlineData("https://fluentvalidation.net", "Invalid Extension")]
        public void InvalidUrls(string url, string errorMessage)
        {
            _newCDN.ValidUrl(url);
            _newCDN.Errors.Should().Contain(p => p.ErrorMessage == errorMessage, "Url Validate is Broken");
        }

        [Fact]
        public void OnlyInvalidExtension()
        {
            _newCDN.ValidUrl("https://fluentvalidation.net");
            _newCDN.Errors.Should().ContainSingle(p => p.ErrorMessage == "Invalid Extension", "Url Validate is Broken");
        }

        [Fact]
        public void ValidUrlAddress()
        {
            var isValid = _newCDN.ValidUrl("https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-01.txt");
            isValid.Should().BeTrue("Url Validate is Broken");
        }

        [Fact]
        public void EmptyLine()
        {
            _newCDN.ValidLine("", out string error);
            error.Should().Contain("Empty Line Content", "Empty Content Validate Is Bronken!");
        }

        [Fact]
        public void IsValidLine()
        {
            var result = _newCDN.ValidLine("312|200|HIT|\"GET /robots.txt HTTP/1.1\"|100.2", out string error);
            result.Should().BeTrue("Valid Line Validate Is Bronken!");
        }

        [Fact]
        public void IsInvalidLine()
        {
            _newCDN.ValidLine("AAA|200|HIT|\"GET /robots.txt HTTP/1.1\"|100.2", out string error);
            error.Should().ContainAny("Invalid Line Content", "Invalid Line Validate Is Bronken!");
        }

        [Theory]
        [InlineData("312|200|HIT|\"GET /robots.txt HTTP/1.1\"|100.2", "\"MINHA CDN\" GET 200 /robots.txt 100 312 HIT")]
        [InlineData("101|200|MISS|\"POST /myImages HTTP/1.1\"|319.4", "\"MINHA CDN\" POST 200 /myImages 319 101 MISS")]
        [InlineData("199|404|MISS|\"GET /not-found HTTP/1.1\"|142.9", "\"MINHA CDN\" GET 404 /not-found 143 199 MISS")]
        [InlineData("312|200|INVALIDATE|\"GET /robots.txt HTTP/1.1\"|245.1", "\"MINHA CDN\" GET 200 /robots.txt 245 312 REFRESH_HIT")]
        public void IsValidConvertion(string myCdnLogLine, string nowLogLine)
        {
            string result = _newCDN.ConvertMyCdnLogLineToNowLogEventLine(myCdnLogLine);
            result.Should().Be(nowLogLine);
        }

        [Fact]
        public void IsValidHeader()
        {
            string version = "1.0";
            DateTime dateTime = DateTime.Now;
            var header = _newCDN.CreateNowLogFileHeader(version, dateTime);
            var validHeader = ValidHeader(version, dateTime);
            header.Should().Be(validHeader);
        }

        [Theory]
        [InlineData("C:\\", false)]
        [InlineData(".\\outPut\\testo.txt", true)]
        [InlineData("..\\outPut\\testo.txt", true)]
        [InlineData("C:\\outPut\\testo.txt", true)]
        [InlineData("C:\\outPut\\testo.axt", false)]
        public void ValidPath(string path, bool result)
        {
            var valid = _newCDN.ValidOutPutPath(path);
            valid.Should().Be(result);
        }

        [Fact]
        public void IsValidContent()
        {
            string url = "https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-01.txt";
            string outputPath = "./output/minhaCdn1.txt";
            StreamWriter writer = _newCDN.CreateNowLogFile(outputPath);
            _newCDN.CreateNowLogEvents(url, writer);
           // StreamReader reader = new StreamReader(writer);           
        }
    }
}