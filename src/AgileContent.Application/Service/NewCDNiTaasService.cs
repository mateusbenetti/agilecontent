using System;
using AgileContent.Application.Interface;
using AgileContent.Domain.NewCDNiTaas.DTo;
using AgileContent.Domain.NewCDNiTaas.Interface;
using FluentValidation.Results;

namespace AgileContent.Application.Service
{
    public class NewCDNiTaasService : Behaviors.Service, INewCDNiTaasService
    {
        IReadFileContentCommand _readFileContentCommand;
        IConvertCdnToNowLogFileCommand _convertCdnToNowLogFileCommand;
        ICreateNowLogFileContentCommand _createNowLogFileContentCommand;

        public NewCDNiTaasService(IReadFileContentCommand readFileContentCommand,
            IConvertCdnToNowLogFileCommand convertCdnToNowLogFileCommand,
            ICreateNowLogFileContentCommand createNowLogFileContentCommand)
        {
            _readFileContentCommand = readFileContentCommand;
            _convertCdnToNowLogFileCommand = convertCdnToNowLogFileCommand;
            _createNowLogFileContentCommand = createNowLogFileContentCommand;
        }

        public string ConvertCdnFileToNowFile(string sourceUrl, string version, DateTime dateTime)
        {
            string nowFileContent = string.Empty;
            ValidationResult result;
            var logFileDTo = new LogFileDTo() { Url = sourceUrl, Version = version, DateTime = dateTime };
            _readFileContentCommand.SetDto(logFileDTo);
            result = _readFileContentCommand.Valid();
            if (result.IsValid)
            {
                _readFileContentCommand.Execute();
                logFileDTo.FileLines = _readFileContentCommand.GetResult();
                _convertCdnToNowLogFileCommand.SetDto(logFileDTo);
                result = _convertCdnToNowLogFileCommand.Valid();
                if (result.IsValid)
                {
                    _convertCdnToNowLogFileCommand.Execute();
                    logFileDTo.NowLogFileModel = _convertCdnToNowLogFileCommand.GetResult();

                    _createNowLogFileContentCommand.SetDto(logFileDTo);
                    result = _createNowLogFileContentCommand.Valid();
                    if (result.IsValid)
                    {
                        _createNowLogFileContentCommand.Execute();
                        nowFileContent = _createNowLogFileContentCommand.GetResult();
                    }
                }
            }
            if (!result.IsValid)
                Errors = result.Errors;

            return nowFileContent;
        }
    }
}