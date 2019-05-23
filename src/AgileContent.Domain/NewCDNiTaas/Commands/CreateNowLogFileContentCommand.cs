using AgileContent.Domain.Behaviors;
using AgileContent.Domain.NewCDNiTaas.DTo;
using AgileContent.Domain.NewCDNiTaas.Interface;
using AgileContent.Domain.NewCDNiTaas.Validators;

namespace AgileContent.Domain.NewCDNiTaas.Commands
{
    public class CreateNowLogFileContentCommand : CommandHandler<LogFileDTo, CreateNowLogFileContentValidator, string>, ICreateNowLogFileContentCommand
    {
        public override void Execute()
        {
            Result = Dto.NowLogFileModel.FileContent;
        }
    }
}