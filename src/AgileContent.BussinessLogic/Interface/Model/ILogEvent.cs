using AgileContent.BussinessLogic.Model.Enum;

namespace AgileContent.BussinessLogic.Interface.Model
{
    public interface ILogEvent
    {
        string Provider { get; }
        HttpMethod HttpMethod { get; set; }
        string StatusCode { get; set; }
        string UriPath { get; set; }
        string TimeTaken { get; set; }
        string DisplayTimeTaken { get; }
        string ResponseSize { get; set; }
        CacheStatus CacheStatus { get; set; }
        string CacheStatusDescription { get; }
        string FormatLog { get; }
    }
}
