using FluentValidation;
using System.Net;

namespace AgileContent.BussinessLogic.Validators.NewCDN
{
    public class SourceUrlValidator : AbstractValidator<string>
    {
        public SourceUrlValidator()
        {
            RuleFor(p => p).NotEmpty().WithMessage("Url is Empty");
            RuleFor(p => p).Matches(@"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$").WithMessage("Invalid Url");
            RuleFor(p => p).Must(url => url.EndsWith(".txt")).WithMessage("Invalid Extension");
            RuleFor(p => p).Must(HasAccessUrl).WithMessage("Not Access Url");
        }

        public static bool HasAccessUrl(string url)
        {
            bool valid;
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "HEAD";
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                valid = response.StatusCode == HttpStatusCode.OK;
                response.Close();
            }
            catch
            {
                valid = false;
            }
            return valid;
        }
    } 
}