using AgileContent.BussinessLogic.Model.Enum;

namespace AgileContent.BussinessLogic.Model
{
    public class MyCdnLogEventModel : LogEvent
    {
        public MyCdnLogEventModel()
        {
            Provider = "MINHA CDN";
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
