﻿using System.Collections.Generic;

namespace MoneyGuru.WebAPI.Models
{
    public class Wallet
    {
        public int WalletID { get; set; }
        public string Type { get; set; }
        public decimal AmountOfMoney { get; set; }
        public string WalletName { get; set; }
    }
}
