using System;
using System.Collections.Generic;
using System.Text;
using AgileContent.Model.Enum;
namespace AgileContent.Model.Entities
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
                        return "-";
                }
            }
        }

        public override string FormatLog =>
            $"\"{Provider}\" {HttpMethod.ToString().ToUpper()} {StatusCode} {UriPath} {DisplayTimeTaken} {ResponseSize} {CacheStatusDescription}";

        public override string DisplayTimeTaken => TimeTaken.Split(".")[0];
    }
}
