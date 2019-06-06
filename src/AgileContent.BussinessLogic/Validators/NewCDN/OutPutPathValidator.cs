using FluentValidation;
using System.IO;

namespace AgileContent.BussinessLogic.Validators.NewCDN
{
    public class OutPutPathValidator : AbstractValidator<string>
    {
        public OutPutPathValidator()
        {
            RuleFor(p => p).NotEmpty().WithMessage("Url is Empty");
            RuleFor(p => p).Must(url => url.EndsWith(".txt")).WithMessage("Invalid Extension");
            RuleFor(p => p).Must(ValidOutputPath).WithMessage("Not Access Url");
        }

        public static bool ValidOutputPath(string outputPath)
        {
            bool valid = outputPath.IndexOfAny(Path.GetInvalidPathChars()) == -1;
            return valid;
        }
    } 
}