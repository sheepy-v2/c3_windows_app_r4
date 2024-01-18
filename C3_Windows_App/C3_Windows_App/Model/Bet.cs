using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3_Windows_App.Model
{
    internal class Bet
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MatchId { get; set; }
        public int TeamId { get; set; }
        public int Amount { get; set; }
        public bool Payed { get; set; }
        public Bet(int userId, int matchId, int teamId, int amount, bool payed)
        {
            UserId = userId;
            MatchId = matchId;
            TeamId = teamId;
            Amount = amount;
            Payed = payed;
        }
    }
}
