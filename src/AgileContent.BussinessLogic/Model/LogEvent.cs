using AgileContent.BussinessLogic.Interface.Model;
using AgileContent.BussinessLogic.Model.Enum;

namespace AgileContent.BussinessLogic.Model
{
    public abstract class LogEvent : ILogEvent
    {
        public string Provider { get; set; }
        public HttpMethod HttpMethod { get; set; }
        public string StatusCode { get; set; }
        public string UriPath { get; set; }
        public string TimeTaken { get; set; }
        public string ResponseSize { get; set; }
        public CacheStatus CacheStatus { get; set; }
        public abstract string CacheStatusDescription { get; }
        public abstract string FormatLog { get; }
        public abstract string DisplayTimeTaken { get; }
    }
}