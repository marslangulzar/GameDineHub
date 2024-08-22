using BusinessEntities.Common;
using BusinessEntities.Entities.Game;
using BusinessServices.Account;
using BusinessServices.Game;
using GameDineHub.Hubs;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GameDineHub.Controllers
{

    public class GeneralController : BaseController
    {
        //Below code is commented after VCR feedback(Date 2020-12-2-02) .
        //private static SpotifyClient spotify;
        //private static SpotifyAPI.Web.AuthorizationCodeTokenResponse token = new AuthorizationCodeTokenResponse();
        private readonly ITeamServices _teamServices;
        private readonly ISystemSettingServices _systemSettingServices;


        public List<TeamSongVM> TeamSongs { get; set; }

        public GeneralController()
        {
            _teamServices = new TeamServices();
            _systemSettingServices = new SystemSettingServices();
        }
       
        // GET: General
        public ActionResult Display(string text)
        {
            ViewBag.VotingRoundStatus = _teamServices.GetVotingRoundStatus();
            var siteProp = _systemSettingServices.GetSitePropertiy();
            if (siteProp != null)
            {
                ViewBag.SitePropertyID = siteProp.ID;
                TempData["ThemeCast"] = siteProp.Name;
            }
            TeamSongs = _teamServices.GetVotingSongForAdminDisplay();
            return View(TeamSongs);
        }

        [System.Web.Mvc.Authorize]
        public ActionResult Manage()
        {
            var siteProp = _systemSettingServices.GetSitePropertiy();
            if (siteProp != null)
            {
                ViewBag.SitePropertyID = siteProp.ID;
                TempData["Message"] = siteProp.Name;
            }
            TeamVM teamVM = new TeamVM();
            TempData["VotingRoundStatus"] = _teamServices.GetVotingRoundStatus();
            var songsForVote = _teamServices.GetVotingSongForAdminDisplay();
            ViewBag.SongsCount = songsForVote.Count;
            teamVM.teamSongList = _teamServices.GetVotingSongForAdminDisplay();
            //Below code is commented after VCR feedback(Date 2020-12-2-02) .

            //bool IsSpotifyLogin = false;
            //if (token.AccessToken!=null)
            //{
            //    IsSpotifyLogin = true;
            //}
            //teamVM.IsSpotifyLogin = IsSpotifyLogin;
            return View(teamVM);
        }
        //Below code is commented after VCR feedback(Date 2020-12-2-02) .

        //[System.Web.Mvc.Authorize]
        //public ActionResult GetSpotifyToken()
        //{

        //    if (token.IsExpired )
        //    {
        //        var result = GetAccessToken();
        //        return Redirect(result);
        //    }
        //    return RedirectToAction("index");
        //}
        public ActionResult GetVotingSongForAdminDisplay()
        {
            try
            {
                TeamSongs = _teamServices.GetVotingSongForAdminDisplay();
                return PartialView("~/Views/General/_VotingViewScreen.cshtml", TeamSongs);

            }
            catch (Exception ex)
            {
                return Json(GeneralUtilities.JsonErrorMessage(ex), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetVotingSongForDisplay()
        {
            try
            {
                TeamSongs = _teamServices.GetVotingSongForAdminDisplay();
                return PartialView("~/Views/General/_CastVotingViewScreen.cshtml", TeamSongs);

            }
            catch (Exception ex)
            {
                return Json(GeneralUtilities.JsonErrorMessage(ex), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ThemeCast(int? ID,string name)
        {
            try
            {
                if (_currentUserInfo==null || _currentUserInfo.UserId<=0)
                {
                    return Json(new { status = false, message = "Please login again." }, JsonRequestBehavior.AllowGet);
                }
                _systemSettingServices.UpsertSiteProperty(ID,name,_currentUserInfo.UserId);

                var notificationContext = GlobalHost.ConnectionManager.GetHubContext<TeamHub>();
                var siteProp = _systemSettingServices.GetSitePropertiy();
                if (siteProp != null)
                {
                    ViewBag.SitePropertyID = siteProp.ID;
                    TempData["ThemeCast"] = siteProp.Name;
                    notificationContext.Clients.All.publishThemeCast(name);
                }
                

                return Json(new { ID=siteProp!=null?siteProp.ID:0, status = true, message = "Publish Successfully." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(GeneralUtilities.JsonErrorMessage(ex), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Voting(bool startVoting)
        {
            try
            {
                var siteProp = _systemSettingServices.GetSitePropertiy();
                if (siteProp != null)
                {
                    ViewBag.SitePropertyID = siteProp.ID;
                    TempData["ThemeCast"] = siteProp.Name;
                }
                var notificationContext = GlobalHost.ConnectionManager.GetHubContext<TeamHub>();
                if (startVoting)
                {
                    notificationContext.Clients.All.votingStart();
                    _teamServices.StartVoting();

                    return Json(new { status = true, message = "Voting start successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    
                    notificationContext.Clients.All.endVoting();
                    //notificationcontext.clients.all.getteamnameview();
                    _teamServices.EndVoting();
                    return Json(new { status = true, message = "Voting end successfully" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                return Json(new { status = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        //#endregion
    }
}