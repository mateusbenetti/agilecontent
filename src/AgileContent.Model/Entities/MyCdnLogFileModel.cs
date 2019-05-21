using System.Text;

namespace AgileContent.Model.Entities
{
    public class MyCdnLogFileModel : LogFileModel
    {
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
