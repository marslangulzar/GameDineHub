using AutoMapper;
using BusinessEntities.Entities;
using BusinessEntities.Entities.Account;
using DataModel.Model;

namespace BusinessServices.Mapper
{
    public class MappingProfileService : Profile
    {
        public MappingProfileService()
        {
            CreateMap<UserAccountVM, UserInfo>();
            CreateMap<UserInfo, UserAccountVM>();
            CreateMap<UserInfo, UserVM>();
            CreateMap<Role, RoleVM>();
            CreateMap<AppSetting, AppSettingVm>();
           
        }
    }
}
