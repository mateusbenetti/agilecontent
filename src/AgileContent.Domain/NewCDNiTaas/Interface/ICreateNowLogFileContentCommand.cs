﻿using AgileContent.Domain.Behaviors.Interface;
using AgileContent.Domain.NewCDNiTaas.DTo;
using AgileContent.Domain.NewCDNiTaas.Validators;
using System.Collections.Generic;
using System.IO;

namespace AgileContent.Domain.NewCDNiTaas.Interface
{
    public interface ICreateNowLogFileContentCommand : ICommandHandler<LogFileDTo, CreateNowLogFileContentValidator, string>
    {
    }
}