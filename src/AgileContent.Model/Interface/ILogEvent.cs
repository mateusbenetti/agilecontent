using AgileContent.Model.Enum;

namespace AgileContent.Model.Interface
{
    public interface ILogEvent
    {
        string Provider { get; }
        HttpMethod HttpMethod { get; set; }
        int StatusCode { get; set; }
        string UriPath { get; set; }
        double TimeTaken { get; set; }
        int ResponseSize { get; set; }
        CacheStatus CacheStatus { get; set; }
        string CacheStatusDescription { get; }
        string FormatLog { get; }
    }
}
