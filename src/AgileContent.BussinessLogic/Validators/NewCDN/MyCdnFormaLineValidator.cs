using FluentValidation;
using System.Text.RegularExpressions;

namespace AgileContent.BussinessLogic.Validators.NewCDN
{
    public class MyCdnFormaLineValidator : AbstractValidator<string>
    {
        private const string RegexLineValidate = @"^[0-9]{1,3}[|][0-9]{1,3}[|](?:MISS|HIT|INVALIDATE)[|][""](?:GET|POST|PUT|DELETE)\s[\/][a-zA-Z0-9.-]+?\sHTTP[\/][0-9]{1,1}[.][0-9]{1,1}.*[""][|]+[0-9]{1,3}[.][0-9]{1,1}$";
        public MyCdnFormaLineValidator()
        {
            RuleFor(p => p.Length).GreaterThan(0).WithMessage("Empty Line Content");
            RuleFor(p => p).Must(IsValidLine).WithMessage("Invalid Line Content");
        }

        public bool IsValidLine(string line)
        {
            var match = Regex.Match(line, RegexLineValidate);
            return match.Success;
        }
    }
}
