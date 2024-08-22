using BusinessEntities.Common;
using BusinessEntities.Entities.Account;
using BusinessEntities.Entities.Game;
using BusinessServices.Account;
using BusinessServices.Game;
using GameDineHub.Hubs;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using SpotifyAPI.Web; //Base Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace GameDineHub.Controllers
{
    public class TeamController : BaseController
    {
        private readonly ITeamServices _teamServices;
       

        public List<TeamSongVM> TeamSongs { get; set; }
        private readonly ISystemSettingServices _systemSettingServices;

        public TeamController()
        {
            _teamServices = new TeamServices();
            _systemSettingServices = new SystemSettingServices();

        }
        // GET: Team
        public ActionResult Index()
        {
            var siteProp = _systemSettingServices.GetSitePropertiy();
            if (siteProp != null)
            {
                TempData["ThemeCast"] = siteProp.Name;
            }

            var teamAlreadyInRound = false;
            var teamLoggedIn = false;
            var ownTeamSong = "";
            TeamSongs = new List<TeamSongVM>();
            if (_currentUserInfo != null)
            {
                var teamId = Convert.ToInt32(_currentUserInfo.TeamId);
                var teamSong = _teamServices.GetVotingSongByTeamID(teamId);
                teamLoggedIn = true;
                ownTeamSong= teamSong != null ? teamSong.Name : "";
                teamAlreadyInRound = teamSong != null ? true : false;
                TeamSongs = _teamServices.GetVotingSong(teamId);
            }
            ViewBag.TeamState = teamAlreadyInRound;
            ViewBag.TeamLoggedIn = teamLoggedIn;
            ViewBag.OwnTeamSong = ownTeamSong;
            ViewBag.VotingRoundStatus = _teamServices.GetVotingRoundStatus();
            return View(TeamSongs);
        }

        public async Task<ActionResult> CastVote(int songId, int? fromTeamId, int toTeamId)
        {
            try
            {
                if (fromTeamId == null)
                {
                    fromTeamId = _currentUserInfo.TeamId;
                }
                _teamServices.MarkVote(songId, fromTeamId, toTeamId);
                var notificationContext = GlobalHost.ConnectionManager.GetHubContext<TeamHub>();
                notificationContext.Clients.All.refreshAdminView(false);
                return Json(new { status = true, message = "Vote submitted successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GetVotingSong(int? fromTeamId)
        {
            try
            {
                if (fromTeamId == null)
                {
                    fromTeamId = _currentUserInfo==null?0: _currentUserInfo.TeamId;
                }
                if (fromTeamId > 0)
                {
                    TeamSongs = _teamServices.GetVotingSong(fromTeamId);
                }
                else
                {
                    TeamSongs = new List<TeamSongVM>();
                }
              
                return PartialView("~/Views/Team/_VotingList.cshtml", TeamSongs);

            }
            catch (Exception ex)
            {
                return Json(GeneralUtilities.JsonErrorMessage(ex), JsonRequestBehavior.AllowGet);
            }
        }
        //public async Task<string> GetTeamName(int? fromTeamId)
        //{
        //    try
        //    {
        //        string teamName = "";
        //        if (fromTeamId == null)
        //        {
        //            fromTeamId = _currentUserInfo.TeamId;
        //        }
        //        if (fromTeamId > 0)
        //        {
        //            teamName = await _teamServices.GetTeamName(fromTeamId);
        //            ViewBag.teamName = teamName;
        //        }
        //        return teamName;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;// Json(GeneralUtilities.JsonErrorMessage(ex), JsonRequestBehavior.AllowGet);
        //    }
        //}
        public async Task<ActionResult> RegisterTeam(string teamName)
        {
            try
            {    //var notificationContext = GlobalHost.ConnectionManager.GetHubContext<TeamHub>();
                 //notificationContext.Clients.All.funcationName(teamName);

                var teamID = _teamServices.InsertTeam(teamName);
                #region setClaims
                var userInfoData = new UserInfoVM
                {
                    TeamId = teamID
                };
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string serializeUserData = serializer.Serialize(userInfoData);
                //Logged in successfully
                var ident = new ClaimsIdentity(
                new[] { 
                                      // adding following 2 claim just for supporting default antiforgery provider
                                      new Claim(ClaimTypes.UserData, serializeUserData)

                }, DefaultAuthenticationTypes.ApplicationCookie);

                HttpContext.GetOwinContext().Authentication.SignIn(new Microsoft.Owin.Security.AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTime.Now.AddMinutes(50) }, ident);
                #endregion
                return Json(teamID, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { status = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        public async Task<ActionResult> SaveTeamSong(int teamID, string songUrl, string songName,string externalSongUrl)
        {
            try
            {
                var notificationContext = GlobalHost.ConnectionManager.GetHubContext<TeamHub>();
                var _songID =  _teamServices.InsertTeamSong(teamID,songUrl,songName, externalSongUrl);
                var _songObj = _teamServices.GetVotingSongByTeamID(teamID);
                // Update 
               // notificationContext.Clients.All.SetOwnSong(_songObj.Name);
                var votingIsStart = _teamServices.GetVotingRoundStatus();
                var startbuttonEnable = votingIsStart?false:true;
                notificationContext.Clients.All.refreshAdminView(startbuttonEnable);
                if (_teamServices.GetVotingRoundStatus())
                {
                    notificationContext.Clients.All.refreshTeamVotingView();
                }
                //var _votingStatus = _teamServices.GetVotingSongByTeamID(teamID);
                return Json(new { votingStaus= _songObj, songID = _songID ,songObj= _songObj }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { status = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        public async Task<ActionResult> SearchSongs(string searchParam)
        {
            try
            {
                var config = SpotifyClientConfig.CreateDefault();

                var request = new ClientCredentialsRequest("ID5ddArslan271997f0ad2456c48bdb2719979be4ad46c40bbab", "S692407b39c1642aArslan2719974b66953c27199754c78ca1a");
                var response = await new OAuthClient(config).RequestToken(request);

                var _spotify = new SpotifyClient(config.WithToken(response.AccessToken));
               
                    
                ISearchClient sc = _spotify.Search;
                SearchRequest searchRequest = new SearchRequest(SearchRequest.Types.Track, searchParam);
                searchRequest.Limit = 50;
                var resul = await sc.Item(searchRequest);
                var result = resul.Tracks.Items.Where(y=> y.Explicit == false).Select(x => new { id = x.Uri, text = x.Name + "  by " + x.Artists.FirstOrDefault().Name, attr1 = x.ExternalUrls.FirstOrDefault() }).ToList();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw;
            }


        }
       
    }
}