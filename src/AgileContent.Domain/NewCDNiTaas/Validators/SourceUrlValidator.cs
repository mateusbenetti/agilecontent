using AgileContent.Domain.NewCDNiTaas.DTo;
using FluentValidation;

namespace AgileContent.Domain.NewCDNiTaas.Validators
{
    public class SourceUrlValidator : AbstractValidator<LogFileDTo>
    {
        public SourceUrlValidator()
        {
            RuleFor(p => p.Url).NotEmpty().WithMessage("Url is Empty");
            RuleFor(p => p.Url).Matches(@"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$").WithMessage("Invalid Url");
            RuleFor(p => p.Url).Must(url => url.EndsWith(".txt")).WithMessage("Invalid Extension");
        }
    } 
}