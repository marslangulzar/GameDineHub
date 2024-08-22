using BusinessEntities.Entities.Game;
using DataModel.Model;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Game
{
    public class TeamServices : ITeamServices
    {
        private readonly UnitOfWork _unitOfWork;
        /// <summary>
        /// Public constructor.
        /// </summary>
        public TeamServices()
        {
            _unitOfWork = new UnitOfWork();
        }
        #region Team
        public int InsertTeam(string teamName)
        {
            try
            {
                var addTeam = new Team();
                addTeam.Name = teamName;
                addTeam.fk_VotingRoundID = GetLatestVotingRound();
                addTeam.Active = true;
                addTeam.CreatedDate = DateTime.Now;
                addTeam.ModifiedDate = DateTime.Now;
                _unitOfWork.TeamRepository.Insert(addTeam);
                _unitOfWork.Save();
                return addTeam.ID;
            }
            catch (Exception ex)
            {

                throw new Exception("Error on saving team." + ex.Message);
            }
        }
        public int InsertTeamSong(int teamId, string songUrl, string songName, string externalSongUrl)
        {
            try
            {
                var addTeamSong = new TeamSong();
                addTeamSong.Name = songName;
                addTeamSong.fk_TeamID = teamId;
                addTeamSong.SongURL = songUrl;
                addTeamSong.CreatedDate = DateTime.Now;
                addTeamSong.SpotifySongUrl = externalSongUrl;
                addTeamSong.VotingDone = false;
                _unitOfWork.TeamSongRepository.Insert(addTeamSong);
                _unitOfWork.Save();
                return addTeamSong.ID;
            }
            catch (Exception ex)
            {

                throw new Exception("Error on saving team." + ex.Message);
            }
        }
        #endregion
        #region Voting
        public TeamSongVM GetVotingSongByTeamID(int fromTeamID)

        {
            var teamSong = _unitOfWork.TeamSongRepository.GetMany(s => s.VotingDone != true && s.fk_TeamID == fromTeamID).OrderByDescending(x => x.ID).Select(x => new TeamSongVM()
            {
                Name = x.Name,
                SongURL = x.SongURL,
                TeamName = x.Team?.Name,
                fk_TeamID = x.fk_TeamID,
                ID = x.ID
            }).FirstOrDefault();

            return teamSong;
        }
        public List<TeamSongVM> GetVotingSong(int? fromTeamID)
        {
            var teamSongs = _unitOfWork.TeamSongRepository.GetMany(s => s.VotingDone != true && s.fk_TeamID != fromTeamID).Select(x => new TeamSongVM()
            {
                Name = x.Name,
                SongURL = x.SongURL,
                TeamName = x.Team.Name,
                fk_TeamID = x.fk_TeamID,
                ID = x.ID,
                VoteMark = x.VotingResults.Any(y => y.fk_TeamSongID == x.ID && y.fk_VotingByTeamID == fromTeamID)
            }).ToList();

            //if (teamSongs!=null)
            //{
            //    var results = _unitOfWork.VotingResultRepository.GetAll();
            //    foreach (var item in teamSongs)
            //    {
            //        item.VoteMark = results.Any(y => y.fk_VotingByTeamID == fromTeamID && y.fk_TeamSongID == item.ID);
            //        //var canVote = true;
            //        //foreach (var item1 in results.Where(s=>s.fk_TeamSongID==item.ID))
            //        //{
            //        //    canVote = !(item1.fk_VotingByTeamID == fromTeamID);
            //        //}
            //        //item.VoteMark = !canVote;
            //    }
            //}


            return teamSongs;
        }
        //public async Task<string> GetTeamName(int? fromTeamId)
        //{
        //    var result = _unitOfWork.TeamRepository.GetByID(fromTeamId);
        //    if (result != null)
        //    {
        //        return result.Name;
        //    }
        //    return string.Empty;
        //}
        public List<TeamSongVM> GetVotingSongForAdminDisplay()
        {
            var teamSongs = _unitOfWork.TeamSongRepository.GetMany(s => s.VotingDone != true).Select(x => new TeamSongVM()
            {
                Name = x.Name,
                SongURL = x.SongURL,
                TeamName = x.Team.Name,
                fk_TeamID = x.fk_TeamID,
                ExternalUrl = x.SpotifySongUrl,
                VoteCount = x.VotingResults.Count(),
                ID = x.ID
            }).ToList();

            return teamSongs;
        }
        public bool StartVoting()
        {
            var round = _unitOfWork.VotingRoundRepository.GetMany(s => s.VoatingDone != true).FirstOrDefault();
            if (round != null)
            {
                round.StartDate = DateTime.Now;
                round.VoatingStart = true;


            }
            else
            {
                var addRound = new VotingRound();
                addRound.VoatingDone = false;
                addRound.CreatedDate = DateTime.Now;
                addRound.VoatingStart = true;
                addRound.StartDate = DateTime.Now;
                _unitOfWork.VotingRoundRepository.Insert(addRound);
            }
            _unitOfWork.Save();
            return true;
        }
        public bool EndVoting()
        {
            var teamSongs = _unitOfWork.TeamSongRepository.GetMany(s => s.VotingDone != true);
            foreach (var song in teamSongs)
            {
                song.VotingDone = true;
            }
            var rond = _unitOfWork.VotingRoundRepository.GetMany(s => s.VoatingDone != true && s.VoatingStart == true).FirstOrDefault();
            rond.VoatingDone = true;
            _unitOfWork.Save();
            return true;
        }

        #endregion
        #region Round
        public int GetLatestVotingRound()
        {
            var rond = _unitOfWork.VotingRoundRepository.GetMany(s => s.VoatingDone != true).FirstOrDefault();
            if (rond != null)
            {
                return rond.ID;
            }
            else
            {
                var addRound = new VotingRound();
                addRound.VoatingDone = false;
                addRound.CreatedDate = DateTime.Now;
                _unitOfWork.VotingRoundRepository.Insert(addRound);
                _unitOfWork.Save();
                return addRound.ID;
            }

        }

        public bool GetVotingRoundStatus()
        {
            try
            {
                var round = _unitOfWork.VotingRoundRepository.GetMany(v => v.VoatingStart == true && v.VoatingDone != true).FirstOrDefault();
                if (round != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error on getting round team." + ex.Message);
            }

        }

        public int MarkVote(int songId, int? fromTeamId, int toTeamId)
        {
            try
            {
                var votingResult = new VotingResult();
                votingResult.fk_TeamSongID = songId;
                votingResult.fk_VotingByTeamID = Convert.ToInt32(fromTeamId);
                votingResult.fk_VotingToTeamID = toTeamId;
                votingResult.CreatedDate = DateTime.Now;
                _unitOfWork.VotingResultRepository.Insert(votingResult);
                _unitOfWork.Save();
                return votingResult.ID;
            }
            catch (Exception ex)
            {

                throw new Exception("Error on saving team." + ex.Message);
            }
        }
        #endregion
    }
}
