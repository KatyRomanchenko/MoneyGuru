using MoneyGuru.ViewModels;
using System;
using System.Net;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;

namespace MoneyGuru.ViewModels
{
    public class LoginEntryViewModel
    {
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Password { get; set; }
    }
}