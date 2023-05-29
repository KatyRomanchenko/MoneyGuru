using MoneyGuru.ViewModels;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace MoneyGuru
{
    public class AddWalletViewModel
    {
        [Required]
        public string WalletName { get; set; }

        [Required]
        public decimal AmountOfMoney { get; set; }
        public string Type { get; set; } //Cash or card
    }
}