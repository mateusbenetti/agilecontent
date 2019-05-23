using AgileContent.Domain.NewCDNiTaas.DTo;
using FluentValidation;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AgileContent.Domain.NewCDNiTaas.Validators
{
    public class ConvertCdnToNowLogFileValidator : AbstractValidator<LogFileDTo>
    {
        private const string RegexLineValidate = @"^[0-9]{1,3}[|][0-9]{1,3}[|](?:MISS|HIT|INVALIDATE)[|][""](?:GET|POST|PUT|DELETE)\s[\/][a-zA-Z0-9.-]+?\sHTTP[\/][0-9]{1,1}[.][0-9]{1,1}.*[""][|]+[0-9]{1,3}[.][0-9]{1,1}$";
        public ConvertCdnToNowLogFileValidator()
        {
            RuleFor(p => p.FileLines.Count).GreaterThan(0).WithMessage("Empty File Content");
            RuleFor(p => p.FileLines).Must(IsValidContent).WithMessage("Invalid File Content");
        }

        public bool IsValidContent(IList<string> fileLines)
        {
            bool result = true;
            foreach (var line in fileLines)
            {
                var match = Regex.Match(line, RegexLineValidate);
                if (!match.Success)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
    }
}
