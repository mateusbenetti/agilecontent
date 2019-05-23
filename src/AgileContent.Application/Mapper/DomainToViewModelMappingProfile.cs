using AutoMapper;
using AgileContent.Application.Model;
using AgileContent.Model;
using AgileContent.Model.Entities;

namespace AgileContent.Application.Mapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<MyCdnLogEventModel, MyCdnLogEventViewModel>();
        }
    }
}
