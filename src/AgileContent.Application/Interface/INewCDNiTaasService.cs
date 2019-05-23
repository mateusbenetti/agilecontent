using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AgileContent.Application.Interface.Behaviors;
using AgileContent.Application.Model;

namespace AgileContent.Application.Interface
{
    public  interface INewCDNiTaasService : IService
    {
        string ConvertCdnFileToNowFile(string sourceUrl, string version, DateTime dateTime);
    }
}
