using System;
using System.Collections.Generic;
using System.Text;
using AgileContent.Model.Enum;

namespace AgileContent.Model.Entities
{
    public class MyCdnLogEventModel : LogEvent
    {
        public MyCdnLogEventModel()
        {
            Provider = "Minha CDN";
        }

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
                        return "INVALIDATE";
                    default:
                        return "-";
                }
            }
        }

        public override string FormatLog => $"{ResponseSize}|{StatusCode}|{CacheStatusDescription}|\"{HttpMethod.ToString().ToUpper()} {UriPath}  HTTP/1.1\" | {DisplayTimeTaken}";

        public override string DisplayTimeTaken => TimeTaken;
    }
}
