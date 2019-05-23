using System;
using System.Collections.Generic;
using AgileContent.Model.Interface;

namespace AgileContent.Model.Entities
{
    public abstract class LogFileModel<TLogEvent> : ILogFile<TLogEvent> 
        where TLogEvent : ILogEvent
    {
        public string Version { get; set; }
        public DateTime Date { get; set; }
        public string FieldsDescription{ get; set; }
        public List<TLogEvent> Events { get; set; }
        public abstract string FileContent { get;  }
    }
}
