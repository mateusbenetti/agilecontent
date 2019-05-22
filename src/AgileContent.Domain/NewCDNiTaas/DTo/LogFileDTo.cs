using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AgileContent.Domain.NewCDNiTaas.DTo
{
    public class LogFileDTo
    {
        public string Url { get; set; }

        public IList<string> FileLines { get; set; }
    }
}