using System;
using System.Collections.Generic;
using AgileContent.Application.Interface;
using AgileContent.Application.Model;

namespace AgileContent.Application.Service
{
    public class NewCDNiTaasService : INewCDNiTaasService
    {
        public bool ValidSourceUrl(string sourceUrl)
        {
            throw new NotImplementedException();
        }

        public bool ValidUrlFileExtension(string sourceUrl)
        {
            throw new NotImplementedException();
        }

        public string ReadFileContent(string sourceUrl)
        {
            throw new NotImplementedException();
        }

        public bool ValidFileContent(string fileContent)
        {
            throw new NotImplementedException();
        }

        public IList<MyCdnLogEventViewModel> ReadLogContent(string fileContent)
        {
            throw new NotImplementedException();
        }

        public string ConvertNowLogFile(IList<MyCdnLogEventViewModel> events)
        {
            throw new NotImplementedException();
        }
    }
}
