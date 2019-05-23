using System;
using System.Collections.Generic;

namespace AgileContent.Model.Interface
{
    public interface ILogFile<TLogEvent> where TLogEvent : ILogEvent
    {
        string Version { get; set; }
        DateTime Date { get; set; }
        string FieldsDescription { get; set; }
        List<TLogEvent> Events { get; set; }
        string FileContent { get; }
    }
}
