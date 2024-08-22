using BusinessEntities.Entities.Account;
using BusinessEntities.Entities.Common;
using BusinessEntities.Enums;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FireStreetPizza.Model;
using BusinessEntities.Entities.Game;

namespace FireStreetPizza.Controllers
{
    /// <summary>
    /// Class BaseController.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class BaseController : Controller
    {
        #region Services
        public UserInfoVM _currentUserInfo;
        public TeamVM _teamVM;

        #endregion

        #region Constructor
        /// <summary>
        /// Public constructor.
        /// </summary>
        public BaseController()
        {
            _currentUserInfo = SecurityClaims.GetLoggedInUserDetail();
            //_teamVM= SecurityClaims.GetTeamDetail();
        }
        #endregion

        #region File Utility
        public static ResponseVM CreateFileInSystem(HttpPostedFileBase file, string directory = "", string browser = "", string previousPathToRemove = "")
        {
            var response = new ResponseVM() { StatusCode = (int)StatusCodes.Error, StatusMessage = "Error Occur while uploading file. Please contact support" };
            var supportedTypes = new[] { ".jpg", ".jpeg", ".png", ".pdf" };
            try
            {
                string fname;
                fname = file.FileName;
                string fileExtension = fname.Substring(fname.LastIndexOf('.'));
                if (!supportedTypes.Contains(fileExtension.ToLower()))
                {
                    response.StatusCode = (int)StatusCodes.Error;
                    response.StatusMessage = "File Extension Is InValid. Please upload the required formaat.";
                    return response;
                }
                if (browser == "IE" || browser == "INTERNETEXPLORER")
                {
                    string[] testfiles = file.FileName.Split(new char[] { '\\' });
                    fname = testfiles[testfiles.Length - 1];
                }
                else
                {
                    fname = DateTime.Now.Ticks + fname;
                }
                Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(directory));
                string savedFileName = System.Web.HttpContext.Current.Server.MapPath(directory);
                string filename = directory + "/" + fname;
                savedFileName = Path.Combine(savedFileName, fname);
                //RemoveFileWithPath(previousPathToRemove);
                file.SaveAs(savedFileName);
                response.StatusCode = (int)StatusCodes.Success;
                response.StatusMessage = "File uploaded successfully.";
                response.data = filename;
                return response;
            }

            catch (Exception e)
            {
                return response;
            }
        }
        public static void RemoveFileWithPath(string path, bool removeFolder = false)
        {
            try
            {
                path = System.Web.HttpContext.Current.Server.MapPath(path);
                if (removeFolder)
                {
                    var dir = new DirectoryInfo(path);
                    dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
                    dir.Delete(true);
                }
                else
                {
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }
            }
            catch (IOException ioExp)
            {
                Console.WriteLine(ioExp.Message);
            }

        }
        #endregion
    }
}