using BusinessEntities.Entities;
using BusinessEntities.Entities.AppSetting;
using System.Collections.Generic;

namespace BusinessServices.Account
{
    public interface ISystemSettingServices
    {
        List<AppSettingVm> GetAppSetting();
        AppSettingVm GetAppSettingByID(int ID);
        AppSettingVm GetAppSettingByName(string name);
        int UpsertSiteProperty(int? ID,string name,int createdBy);
        SitePropertiyVM GetSitePropertiy();
        bool UpdateAppSetting(List<AppSettingVm> appSettings);
    }
}
