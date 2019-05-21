﻿using AgileContent.Domain.Behaviors.Interface;
using AgileContent.Domain.NewCDNiTaas.DTo;
using AgileContent.Domain.NewCDNiTaas.Validators;

namespace AgileContent.Domain.NewCDNiTaas.Interface
{
    public interface IConvertCdnToNowLogFileCommand : ICommandHandler<LogFileDTo, ConvertCdnToNowLogFileValidator, string>
    {
    }
}