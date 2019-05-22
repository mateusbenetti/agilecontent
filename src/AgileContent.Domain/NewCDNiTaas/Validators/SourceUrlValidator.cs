using AgileContent.Domain.NewCDNiTaas.DTo;
using FluentValidation;
using System;
using System.Net;

namespace AgileContent.Domain.NewCDNiTaas.Validators
{
    public class SourceUrlValidator : AbstractValidator<LogFileDTo>
    {
        public SourceUrlValidator()
        {
            RuleFor(p => p.Url).NotEmpty().WithMessage("Url is Empty");
            RuleFor(p => p.Url).Matches(@"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$").WithMessage("Invalid Url");
            RuleFor(p => p.Url).Must(url => url.EndsWith(".txt")).WithMessage("Invalid Extension");
            RuleFor(p => p.Url).Must(HasAccessUrl).WithMessage("Not Access Url");
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