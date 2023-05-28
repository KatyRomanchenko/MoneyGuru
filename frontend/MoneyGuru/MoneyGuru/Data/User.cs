using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyGuru.Data
{
    public class User
    {
        public List<Transaction> Transactions { get; set; }
        public List<Category> Categories { get; set; }
        public List<Goal> Goals { get; set; }
        public List<Wallet> Wallets { get; set; }
        public string Hierarchy { get; set; }
    }
}
