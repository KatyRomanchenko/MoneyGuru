using MoneyGuru.ViewModels;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace MoneyGuru
{
    public class AddTransactionViewModel
    {
        [Required]
        public string Category { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string TransactionType { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }

}