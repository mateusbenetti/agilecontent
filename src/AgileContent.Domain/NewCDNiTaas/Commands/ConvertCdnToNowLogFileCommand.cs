using AgileContent.Domain.Behaviors;
using AgileContent.Domain.NewCDNiTaas.DTo;
using AgileContent.Domain.NewCDNiTaas.Interface;
using AgileContent.Domain.NewCDNiTaas.Validators;
using AgileContent.Model.Entities;
using AgileContent.Model.Enum;
using System;

namespace AgileContent.Domain.NewCDNiTaas.Commands
{
    public class ConvertCdnToNowLogFileCommand : CommandHandler<LogFileDTo, ConvertCdnToNowLogFileValidator, NowLogFileModel>, IConvertCdnToNowLogFileCommand
    {
        public override void Execute()
        {
            Result = new NowLogFileModel(Dto.Version, Dto.DateTime);
            var myCdnLogFileModel = new MyCdnLogFileModel();
            foreach (var line in Dto.FileLines)
                myCdnLogFileModel.Events.Add(BuildMyCdnLogEventModel(line));
            foreach (MyCdnLogEventModel myCdnLogEventModel in myCdnLogFileModel.Events)
                Result.Events.Add(ConvertMyCdnLogEventToNowLogEventModel(myCdnLogEventModel));
        }

        static MyCdnLogEventModel BuildMyCdnLogEventModel(string line)
        {
            var values = line.Split('|');
            var myCdnLogEventModel = new MyCdnLogEventModel()
            {
                ResponseSize = int.Parse(values[0]),
                StatusCode = int.Parse(values[1]),
                CacheStatus = ConvertToCacheStatus(values[2]),
                HttpMethod = GetHttpMethod(values[3]),
                UriPath = UriPath(values[3]),
                TimeTaken = values[4]
            };
            return myCdnLogEventModel;
        }

        static CacheStatus ConvertToCacheStatus(string value)
        {
            CacheStatus cacheStatus;
            switch (value)
            {
                case "HIT":
                    cacheStatus = CacheStatus.Hit;
                    break;
                case "MISS":
                    cacheStatus = CacheStatus.Miss;
                    break;
                case "INVALIDATE":
                    cacheStatus = CacheStatus.RefreshHit;
                    break;
                default:
                    cacheStatus = CacheStatus.None;
                    break;
            }
            return cacheStatus;
        }
        static HttpMethod GetHttpMethod(string value)
        {
            HttpMethod httpMethod;
            string httpMethodValue = value.Trim('"').Split(' ')[0];
            switch (httpMethodValue)
            {
                case "GET":
                    httpMethod = HttpMethod.Get;
                    break;
                case "POST":
                    httpMethod = HttpMethod.Post;
                    break;
                case "PUT":
                    httpMethod = HttpMethod.Put;
                    break;
                case "DELETE":
                    httpMethod = HttpMethod.Delete;
                    break;
                default:
                    httpMethod = HttpMethod.None;
                    break;
            }
            return httpMethod;
        }

        static NowLogEventModel ConvertMyCdnLogEventToNowLogEventModel(MyCdnLogEventModel myCdnLogEventModel)
        {
            var nowLogEventModel = new NowLogEventModel()
            {
                Provider = myCdnLogEventModel.Provider,
                ResponseSize = myCdnLogEventModel.ResponseSize,
                StatusCode = myCdnLogEventModel.StatusCode,
                CacheStatus = myCdnLogEventModel.CacheStatus,
                HttpMethod = myCdnLogEventModel.HttpMethod,
                UriPath = myCdnLogEventModel.UriPath,
                TimeTaken = myCdnLogEventModel.TimeTaken
            };
            return nowLogEventModel;
        }

        static string UriPath(string value)
        {
            return value.Trim('"').Split(' ')[1];
        }
    }
}
