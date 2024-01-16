using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3_Windows_App.Model
{
    internal class Result
    {
        public int Id { get; set; }
        public int Team1_Id { get; set; }
        public string Team1_Name { get; set; }
        public int Team1_Score { get; set; }
        public int Team2_Id { get; set; }
        public string Team2_Name { get; set; }
        public int Team2_Score { get; set; }
        public int? Winner_Id { get; set; }

        public Result(int id, int team1Id, string team1Name, int team1Score, int team2Id, string team2Name, int team2Score, int? winnerId)
        {
            Id = id;
            Team1_Id = team1Id;
            Team1_Name = team1Name;
            Team1_Score = team1Score;
            Team2_Id = team2Id;
            Team2_Name = team2Name;
            Team2_Score = team2Score;
            Winner_Id = winnerId;
        }
    }
}
