using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AgileContent.Application.Model;

namespace AgileContent.Application.Interface
{
    public  interface INewCDNiTaasService
    {
        bool ValidSourceUrl(string sourceUrl);
        bool ValidUrlFileExtension(string sourceUrl);
        string ReadFileContent(string sourceUrl);
        bool ValidFileContent(string fileContent);
        IList<MyCdnLogEventViewModel> ReadLogContent(string fileContent);
        string ConvertNowLogFile(IList<MyCdnLogEventViewModel> events);
    }
}
