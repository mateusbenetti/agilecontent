using AutoMapper;
using AgileContent.Application.Model;
using AgileContent.Model.Entities;

namespace AgileContent.Application.Mapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<MyCdnLogEventViewModel, MyCdnLogEventModel>();
        }
    }
}
