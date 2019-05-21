using AgileContent.Domain.Behaviors;
using AgileContent.Domain.NewCDNiTaas.DTo;
using AgileContent.Domain.NewCDNiTaas.Interface;
using AgileContent.Domain.NewCDNiTaas.Validators;
using System;

namespace AgileContent.Domain.NewCDNiTaas.Commands
{
    public class ReadFileContentCommand : CommandHandler<LogFileDTo, SourceUrlValidator, string>, IReadFileContentCommand
    {
        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
