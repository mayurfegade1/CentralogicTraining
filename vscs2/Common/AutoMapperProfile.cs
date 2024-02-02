using AutoMapper;
using vscs2.DTO;

using vscs2.Entities;

namespace vscs2.Common
{
    public class AutomapperProfile:Profile
    {
        public AutomapperProfile()
        {
            CreateMap<CompanyUsers, CompanyUsersModel>().ReverseMap();
            CreateMap<Visitor, VisitorModel>().ReverseMap();
            CreateMap<Request, RequestModel>().ReverseMap();
        }

    }
}
