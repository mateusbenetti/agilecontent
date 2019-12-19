using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using AgileContent.BussinessLogic.Interface;
using AgileContent.BussinessLogic.Model;
using AgileContent.BussinessLogic.Model.Enum;
using AgileContent.BussinessLogic.Validators.NewCDN;
using FluentValidation;

namespace AgileContent.BussinessLogic
{
    public class NewCDN : Behaviors.BussinessLogic, INewCDN
    {
        readonly IValidator<string> _sourceUrlValidator;
        readonly IValidator<string> _outPutPathValidator;
        readonly IValidator<string> _myCdnFormaLineValidator;
        public NewCDN()
        {
            _sourceUrlValidator = new SourceUrlValidator();
            _myCdnFormaLineValidator = new MyCdnFormaLineValidator();
            _outPutPathValidator = new OutPutPathValidator();
        }

        public bool ValidUrl(string url)
        {
            var validate = _sourceUrlValidator.Validate(url);
            Errors = validate.Errors;
            return validate.IsValid;
        }
        public bool ValidOutPutPath(string outPutPath)
        {
            var validate = _outPutPathValidator.Validate(outPutPath);
            Errors = validate.Errors;
            return validate.IsValid;
        }

        public void ConvertMyCdnToNow(string url, string outputPath)
        {
            var file = CreateNowLogFile(outputPath);
            CreateNowLogEvents(url, file);
        }

        public StreamWriter CreateNowLogFile(string outputPath)
        {
            var path = outputPath.Split('/');
            string file = path.Last();
            var directory = GetDirectory(path);
            FileStream fileStream = CreateFile(directory, file);
            return new StreamWriter(fileStream);
        }
        public void CreateNowLogEvents(string url, StreamWriter nowLogFile)
        {
            using (var webClient = new WebClient())
            {
                Stream stream = webClient.OpenRead(url);
                using (var streamReader = new StreamReader(stream))
                using (nowLogFile)
                {
                    CreateHeaderNowLogFile(nowLogFile);
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                        nowLogFile.WriteLine(ConvertMyCdnLogLineToNowLogEventLine(line));
                }
            }
        }
        public bool ValidLine(string line, out string error)
        {
            error = string.Empty;
            var validate = _myCdnFormaLineValidator.Validate(line);
            if (!validate.IsValid)
                error = string.Join('|', validate.Errors.Select(p => p.ErrorMessage));
            return validate.IsValid;
        }
        public string ConvertMyCdnLogLineToNowLogEventLine(string myCdnLine)
        {
            if (ValidLine(myCdnLine, out string error))
            {
                var myCdnLogEvent = BuildMyCdnLogEventModel(myCdnLine);
                var nowLogEventModel = new NowLogEventModel()
                {
                    Provider = myCdnLogEvent.Provider,
                    ResponseSize = myCdnLogEvent.ResponseSize,
                    StatusCode = myCdnLogEvent.StatusCode,
                    CacheStatus = myCdnLogEvent.CacheStatus,
                    HttpMethod = myCdnLogEvent.HttpMethod,
                    UriPath = myCdnLogEvent.UriPath,
                    TimeTaken = myCdnLogEvent.TimeTaken
                };
                return nowLogEventModel.FormatLog;
            }
            else
            {
                return error;
            }
        }

        string GetDirectory(string[] path)
        {
            string directory = string.Empty;
            for (int i = 0; i < path.Length - 1; i++)
                directory += path[i] + "/";
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            return directory;
        }
        FileStream CreateFile(string directory, string file)
        {
            var files = Directory.GetFiles(directory);
            if (files.ToList().Exists(p => p.Equals(directory + file)))
                File.Delete(directory + file);
            return new FileStream(directory + file, FileMode.CreateNew);
        }
        void CreateHeaderNowLogFile(StreamWriter nowLogFile)
        {
            var header = CreateNowLogFileHeader("1.0", DateTime.Now);
            nowLogFile.Write(header.ToString());
        }
        public string CreateNowLogFileHeader(string version, DateTime dateTime)
        {
            var fieldsDescription = "provider http-method status-code uri-path time-taken response-size cache-status";
            var builder = new StringBuilder();
            builder.AppendLine($"#Version: {version}");
            builder.AppendLine($"#Date: {dateTime.ToShortDateString()} {dateTime.ToLongTimeString()}");
            builder.AppendLine($"#Fields: {fieldsDescription}");
            return builder.ToString();
        }
        static MyCdnLogEventModel BuildMyCdnLogEventModel(string line)
        {
            var values = line.Split('|');
            var myCdnLogEventModel = new MyCdnLogEventModel()
            {
                ResponseSize = values[0],
                StatusCode = values[1],
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
        static string UriPath(string value)
        {
            return value.Trim('"').Split(' ')[1];
        }
    }
}