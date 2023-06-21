using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp2.Models
{
    public class Team
    {
        public string Name { get; set; }
        public string Event { get; set; }
        public DateOnly CreatedDate { get; set; }
        public double Amount { get; set; }     
        public string CardNo { get; set; }
        public string DeviceId { get; set; }
        public string UserName { get; set; }       
    }

    public class Streak : IComparable
    {
        public Result Result { get; set; }
        public int NumStreak { get; set; }

        public int CompareTo(object obj)
        {
            var score = Result == Result.Won ? NumStreak : -NumStreak;
            if (obj is Streak s)
            {
                var otherScore = s.Result == Result.Won ? s.NumStreak : -s.NumStreak;
                return score - otherScore;
            }

            return score;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{Enum.GetName(typeof(Result), Result)} {NumStreak}";
        }
    }

    public enum Result
    {
        Lost = 0,
        Won = 1
    }
}
