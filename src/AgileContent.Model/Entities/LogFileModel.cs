using System;
using System.Collections.Generic;
using AgileContent.Model.Interface;

namespace AgileContent.Model.Entities
{
    public abstract class LogFileModel : ILogFile
    {
        public double Version { get; set; }
        public DateTime Date { get; set; }
        public string FieldsDescription{ get; set; }
        public List<ILogEvent> Events { get; set; }
        public abstract string FileContent { get;  }
    }
}
