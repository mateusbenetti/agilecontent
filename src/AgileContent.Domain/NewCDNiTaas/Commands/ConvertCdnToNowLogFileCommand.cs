using AgileContent.Domain.Behaviors;
using AgileContent.Domain.NewCDNiTaas.DTo;
using AgileContent.Domain.NewCDNiTaas.Interface;
using AgileContent.Domain.NewCDNiTaas.Validators;
using System;

namespace AgileContent.Domain.NewCDNiTaas.Commands
{
    public class ConvertCdnToNowLogFileCommand : CommandHandler<LogFileDTo, ConvertCdnToNowLogFileValidator, string>, IConvertCdnToNowLogFileCommand
    {
        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
