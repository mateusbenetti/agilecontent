using AgileContent.BussinessLogic.Interface.Behaviors;
using System;
using System.IO;

namespace AgileContent.BussinessLogic.Interface
{
    public  interface INewCDN : IBussinessLogic
    {
        bool ValidUrl(string url);
        bool ValidOutPutPath(string outPutPath);
        void ConvertMyCdnToNow(string url, string outputPath);
    }
}
