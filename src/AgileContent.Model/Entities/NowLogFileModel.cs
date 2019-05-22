using System;
using System.Collections.Generic;
using System.Text;

namespace AgileContent.Model.Entities
{
    public class NowLogFileModel : LogFileModel<NowLogEventModel>
    {
        public NowLogFileModel(string version, DateTime dateTime)
        {
            Version = version;
            FieldsDescription = "provider http-method status-code uri-path time-taken response-size cache-status";
            Date = dateTime;
            Events = new List<NowLogEventModel>();
        }
        public override string FileContent
        {
            get
            {
                var builder = new StringBuilder();
                builder.AppendLine($"#Version: {Version}");
                builder.AppendLine($"#Date: {Date.ToShortDateString()} {Date.ToLongTimeString()}");
                builder.AppendLine($"#Fields: {FieldsDescription}");
                Events.ForEach(p => builder.AppendLine(p.FormatLog));
                return builder.ToString().Trim();
            }
        }
    }
}
