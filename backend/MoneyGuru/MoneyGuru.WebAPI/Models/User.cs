using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace MoneyGuru.WebAPI.Models
{
    public class User : IdentityUser
    {
        public List<Transaction> Transactions { get; set; }
        public List<Category> Categories { get; set; }
        public List<Goal> Goals { get; set; }
        public List<Wallet> Wallets { get; set; }
        public string Hierarchy { get; set; }
    }
}
