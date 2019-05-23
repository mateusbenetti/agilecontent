using AgileContent.Application.Interface;
using AgileContent.Application.Service;
using AgileContent.Domain.NewCDNiTaas.Commands;
using AgileContent.Domain.NewCDNiTaas.Interface;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AgileContent.CrossCutting.Test.NewCDNiTaas.Application
{
    public class NewCDNiTaasServiceApplicationTest
    {
        INewCDNiTaasService _newCDNiTaasService;
        IConvertCdnToNowLogFileCommand _convertCdnToNowLogFileCommand;
        ICreateNowLogFileContentCommand _createNowLogFileContentCommand;
            IReadFileContentCommand _readFileContentCommand;
        public NewCDNiTaasServiceApplicationTest()
        {
            _readFileContentCommand = new ReadFileContentCommand();
            _convertCdnToNowLogFileCommand = new ConvertCdnToNowLogFileCommand();
            _createNowLogFileContentCommand = new CreateNowLogFileContentCommand();
            _newCDNiTaasService = new NewCDNiTaasService(
                _readFileContentCommand, 
                _convertCdnToNowLogFileCommand,
                _createNowLogFileContentCommand);
        }
        [Fact]
        public void ValidConversion()
        {
            _newCDNiTaasService.ConvertCdnFileToNowFile("https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-01.txt", "1.0", DateTime.Now);
            _newCDNiTaasService.Errors.Count.Should().Be(0, "Not possible convert log file.");
        }
    }
}
