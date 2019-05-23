using System;
using AgileContent.Application.Interface.Behaviors;

namespace AgileContent.Application.Interface
{
    public  interface INewCDNiTaasService : IService
    {
        string ConvertCdnFileToNowFile(string sourceUrl, string version, DateTime dateTime);
    }
}
