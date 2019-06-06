using AgileContent.BussinessLogic.Model.Enum;
using System;
using System.Globalization;

namespace AgileContent.BussinessLogic.Model
{
    public class NowLogEventModel : LogEvent
    {
        public override string CacheStatusDescription
        {
            get
            {
                switch (CacheStatus)
                {
                    case CacheStatus.Hit:
                        return "HIT";
                    case CacheStatus.Miss:
                        return "MISS";
                    case CacheStatus.RefreshHit:
                        return "REFRESH_HIT";
                    default:
                        Convert.ToDecimal("1.2345", new CultureInfo("en-US"));
                        return "-";
                }
            }
        }

        public override string FormatLog =>
            $"\"{Provider}\" {HttpMethod.ToString().ToUpper()} {StatusCode} {UriPath} {DisplayTimeTaken} {ResponseSize} {CacheStatusDescription}";

        public override string DisplayTimeTaken
        {
            get
            {
                var decimalNumber = Convert.ToDecimal(TimeTaken, new CultureInfo("en-US"));
                return Math.Round(decimalNumber, 0).ToString();
            }
            
        }
       
    }
}
