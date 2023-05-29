using MoneyGuru.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MoneyGuru.WebAPI
{
    public class AddWalletViewModel
    {
        [Required]
        public string Type { get; set; } //Cash or card
        public decimal AmountOfMoney { get; set; }
        public string WalletName { get; set; }
    }
}