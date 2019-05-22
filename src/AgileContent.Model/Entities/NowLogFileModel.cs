using System;
using System.Text;

namespace AgileContent.Model.Entities
{
    public class NowLogFileModel : LogFileModel
    {
        public NowLogFileModel(double version)
        {
            Version = version;
            FieldsDescription = "provider http-method status-code uri-path time-taken response - size cache - status";
            Date = DateTime.Now;
        }
        public override string FileContent
        {
            get
            {
                var builder = new StringBuilder();
                builder.AppendLine($"#Version: {Version}");
                builder.AppendLine($"#Date: {Date.ToLongDateString()}");
                builder.AppendLine($"#Fields: {FieldsDescription}");
                Events.ForEach(p => builder.AppendLine(p.FormatLog));
                return builder.ToString();
            }
        }
    }
}
