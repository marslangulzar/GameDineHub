using BusinessEntities.Entities.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Game
{
    public interface ITeamServices
    {
        #region Team
        int InsertTeam(string teamName);
        int InsertTeamSong(int teamId, string songUrl, string songName,string externalSongUrl);
        #endregion
        #region Voting
        List<TeamSongVM> GetVotingSong(int? fromTeamId);
        //Task<string> GetTeamName(int? fromTeamId);
        bool StartVoting();
        bool EndVoting();
        int MarkVote(int songId, int? fromTeamId, int toTeamId);
        List<TeamSongVM> GetVotingSongForAdminDisplay();
        TeamSongVM GetVotingSongByTeamID(int fromTeamID);
        #endregion
        #region Round
        bool GetVotingRoundStatus();
        #endregion
    }
}
