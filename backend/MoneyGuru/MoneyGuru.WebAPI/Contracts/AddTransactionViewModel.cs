﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MoneyGuru.WebAPI
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