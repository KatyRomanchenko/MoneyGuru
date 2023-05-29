using System;
using System.Collections.Generic;

namespace MoneyGuru.WebAPI.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public string TransactionName { get; set; }
        public User User { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; }
        public DateTime Date { get; set; }

    }
}
