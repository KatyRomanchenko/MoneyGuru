using System.Collections.Generic;
using System;
using System.Text;
using MoneyGuru;
using MoneyGuru.WebAPI;

namespace MoneyGuru.WebAPI.Contracts
{
    public class UpdateWalletViewModel
    {
        public string WalletName { get; set; }
        public decimal AmountOfMoney { get; set; }
    }
}
