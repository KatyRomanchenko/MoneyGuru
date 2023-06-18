using System;
using System.ComponentModel.DataAnnotations;

namespace MoneyGuru
{
    public class AddTransactionViewModel
    {
        [Required]
        public string Category { get; set; }
        public string TransactionName { get; set; }
        [Required]
        public string Wallet { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string TransactionType { get; set; }

        public DateTime Date { get; set; }

        public string TotalAmount = "12345";
    }
}