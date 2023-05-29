using MoneyGuru.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MoneyGuru.WebAPI
{
    public class AddWalletViewModel
    {
        public int WalletID { get; set; }

        [Required]
        public string Type { get; set; } //Cash or card

        [Required]
        public decimal AmountOfMoney { get; set; }
        public string WalletName { get; set; }
    }
}