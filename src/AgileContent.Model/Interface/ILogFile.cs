using System;
using System.Collections.Generic;

namespace AgileContent.Model.Interface
{
    public interface ILogFile
    {
        double Version { get; set; }
        DateTime Date { get; set; }
        string FieldsDescription { get; set; }
        List<ILogEvent> Events { get; set; }
        string FileContent { get; }
    }
}
