using System.Collections.Generic;
using System.Text;

namespace AgileContent.Model.Entities
{
    public class MyCdnLogFileModel : LogFileModel<MyCdnLogEventModel>
    {
        public MyCdnLogFileModel()
        {
            Events = new List<MyCdnLogEventModel>();
        }
        public override string FileContent
        {
            get
            {
                var builder = new StringBuilder();
                Events.ForEach(p => builder.AppendLine(p.FormatLog));
                return builder.ToString();
            }
        }
    }
}
