using AgileContent.Domain.Behaviors.Interface;
using AgileContent.Domain.NewCDNiTaas.DTo;
using AgileContent.Domain.NewCDNiTaas.Validators;
using AgileContent.Model.Entities;

namespace AgileContent.Domain.NewCDNiTaas.Interface
{
    public interface IConvertCdnToNowLogFileCommand : ICommandHandler<LogFileDTo, ConvertCdnToNowLogFileValidator, NowLogFileModel>
    {
    }
}
