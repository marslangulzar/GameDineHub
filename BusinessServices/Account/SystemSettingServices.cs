using BusinessEntities.Entities;
using BusinessEntities.Entities.AppSetting;
using BusinessServices.Mapper;
using DataModel.Model;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessServices.Account
{
    public class SystemSettingServices : ISystemSettingServices
    {
        private readonly UnitOfWork _unitOfWork;
        /// <summary>
        /// Public constructor.
        /// </summary>
        public SystemSettingServices()
        {
            _unitOfWork = new UnitOfWork();
        }
        /// <summary>
        /// GetAppSetting
        /// </summary>
        /// <returns>List<AppSettingVm></returns>
        public List<AppSettingVm> GetAppSetting()
        {
            List<AppSettingVm> result = new List<AppSettingVm>();
            try
            {
                result = _unitOfWork.AppSettingRepository.GetAll().Select(x => new AppSettingVm() { ID = x.ID, Name = x.Name, Value = x.Value, Description = x.Description, Label = x.Label }).ToList();
            }
            catch { }
            return result;
        }
        /// <summary>
        /// GetAppSetting By ID
        /// </summary>
        /// <returns>AppSettingVmx</returns>
        public AppSettingVm GetAppSettingByID(int ID)
        {
            AppSettingVm result = new AppSettingVm();
            try
            {
                var setting = _unitOfWork.AppSettingRepository.GetByID(ID);
                return Mapping.Mapper.Map<AppSettingVm>(setting);

            }
            catch (Exception ex)
            {
                return result;
            }
        }
        /// <summary>
        /// GetAppSetting By ID
        /// </summary>
        /// <returns>AppSettingVmx</returns>
        public AppSettingVm GetAppSettingByName(string name)
        {
            AppSettingVm result = new AppSettingVm();
            try
            {
                var setting = _unitOfWork.AppSettingRepository.GetFirstOrDefault(u => u.Name.ToUpper() == name.ToUpper());
                return Mapping.Mapper.Map<AppSettingVm>(setting);

            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public SitePropertiyVM GetSitePropertiy()
        {
            SitePropertiyVM result = new SitePropertiyVM();
            try
            {
                var setting = _unitOfWork.SiteProprityRepository.GetFirstOrDefault(a => a.Name != null);
                if (setting != null)
                {
                SitePropertiyVM sitePropertiyVM = new SitePropertiyVM() {
                    ID = setting.ID,
                    Name = setting.Name,
                    CreatedBy = setting.CreatedBy,
                  CreatedDate = setting.CreatedDate
                 };
                return sitePropertiyVM;
                }
                return null;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public bool UpdateAppSetting(List<AppSettingVm> appSettings)
        {
            try
            {
                foreach (var setting in appSettings)
                {
                    var settingObj = _unitOfWork.AppSettingRepository.GetFirstOrDefault(x => x.ID == setting.ID);
                    if (settingObj != null)
                    {
                        settingObj.Value = setting.Value;
                        _unitOfWork.AppSettingRepository.Update(settingObj);
                        _unitOfWork.Save();

                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public int UpsertSiteProperty(int? ID, string name, int createdBy)
        {
            try
            {
                if (ID > 0)
                {

                    var sitePropObj = _unitOfWork.SiteProprityRepository.GetFirstOrDefault(x => x.ID == ID);
                    if (sitePropObj != null)
                    {
                        sitePropObj.Name = name;
                        sitePropObj.CreatedDate = DateTime.Now;
                        sitePropObj.CreatedBy = createdBy;
                        _unitOfWork.SiteProprityRepository.Update(sitePropObj);
                        _unitOfWork.Save();
                    }
                    return 1;
                }
                else
                {
                    SiteProprity sitePropertiyObj = new SiteProprity();

                    sitePropertiyObj.Name = name;
                    sitePropertiyObj.CreatedDate = DateTime.Now;
                    sitePropertiyObj.CreatedBy = createdBy;
                    _unitOfWork.SiteProprityRepository.Insert(sitePropertiyObj);
                    _unitOfWork.Save();

                    return 1;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}