using BusinessEntities.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Entities.Game
{
    public class TeamVM : BaseModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public int fk_VotingRoundID { get; set; }

        //Custom fields
        public bool IsSpotifyLogin { get; set; }

        public List<TeamSongVM> teamSongList;

    }
    public class TeamSongVM : BaseModel
    {
        public int ID { get; set; }
        public int fk_TeamID { get; set; }
        public string Name { get; set; }
        public string SongURL { get; set; }
        public Nullable<bool> VotingDone { get; set; }

        //Custom
        public string TeamName { get; set; }
        public int fk_TeamSongID { get; set; }
        public int hub { get; set; }
        public bool VoteMark { get; set; }
        public int VoteCount { get; set; }
        public string ExternalUrl { get; set; }

    }
    public class VotingResultVM : BaseModel
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<bool> VoatingStart { get; set; }
        public Nullable<bool> VoatingDone { get; set; }
    }
    public class VotingRoundVM : BaseModel
    {
        public int ID { get; set; }
        public int fk_VotingByTeamID { get; set; }
        public int fk_VotingToTeamID { get; set; }
    }
}
