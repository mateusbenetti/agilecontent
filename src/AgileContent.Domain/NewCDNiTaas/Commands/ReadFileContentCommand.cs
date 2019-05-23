using AgileContent.Domain.Behaviors;
using AgileContent.Domain.NewCDNiTaas.DTo;
using AgileContent.Domain.NewCDNiTaas.Interface;
using AgileContent.Domain.NewCDNiTaas.Validators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace AgileContent.Domain.NewCDNiTaas.Commands
{
    public class ReadFileContentCommand : CommandHandler<LogFileDTo, SourceUrlValidator, IList<string>>, IReadFileContentCommand
    {
        public override void Execute()
        {
            IList<string> result = new List<string>();
            try
            {
                using (var webClient = new WebClient())
                {
                    byte[] downloadData = webClient.DownloadData(Dto.Url);
                    Stream stream = new MemoryStream(downloadData);
                    using (var streamReader = new StreamReader(stream))
                    {
                        string line;
                        while ((line = streamReader.ReadLine()) != null)
                            result.Add(line);
                    }
                }
            }
            catch (Exception err)
            {
                throw;
            }
            Result = result;
        }
    }
}
