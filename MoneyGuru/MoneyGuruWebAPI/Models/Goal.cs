using System;
using System.Collections.Generic;

namespace MoneyGuruWebAPI.Models
{
    public class Goal
    {
        public int GoalID { get; set; }
        public User User { get; set; }
        public decimal Amount { get; set; }
        public DateTime Deadline { get; set; }
    }
}
