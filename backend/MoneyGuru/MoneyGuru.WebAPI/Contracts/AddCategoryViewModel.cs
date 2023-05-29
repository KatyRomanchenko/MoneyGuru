using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MoneyGuru.WebAPI
{
    public class AddCategoryViewModel
    {
        [Required]
        public string Name { get; set; }

        public decimal TotalAmount { get; set; }
    }
}