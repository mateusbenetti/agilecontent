using AgileContent.Domain.NewCDNiTaas.DTo;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgileContent.Domain.NewCDNiTaas.Validators
{
    public class CreateNowLogFileContentValidator : AbstractValidator<LogFileDTo>
    {
        public CreateNowLogFileContentValidator()
        {
            RuleFor(p => p.NowLogFileModel.Events.Count).GreaterThan(0).WithMessage("There is not events for build now log file.");
        }
    }
}