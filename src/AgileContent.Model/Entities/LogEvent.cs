using AgileContent.Model.Enum;
using AgileContent.Model.Interface;

namespace AgileContent.Model.Entities
{
    public abstract class LogEvent : ILogEvent
    {
        public string Provider { get; set; }
        public HttpMethod HttpMethod { get; set; }
        public int StatusCode { get; set; }
        public string UriPath { get; set; }
        public string TimeTaken { get; set; }
        public int ResponseSize { get; set; }
        public CacheStatus CacheStatus { get; set; }
        public abstract string CacheStatusDescription { get; }
        public abstract string FormatLog { get; }
        public abstract string DisplayTimeTaken { get; }
    }
}