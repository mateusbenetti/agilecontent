using System;
using System.Collections.Generic;
using System.Text;
using AgileContent.Model.Enum;

namespace AgileContent.Application.Model
{
    public class MyCdnLogEventViewModel
    {
        public string Provider { get; set; }
        public HttpMethod HttpMethod { get; set; }
        public int StatusCode { get; set; }
        public string UriPath { get; set; }
        public double TimeTaken { get; set; }
        public int ResponseSize { get; set; }
        public CacheStatus CacheStatus { get; set; }
    }
}
