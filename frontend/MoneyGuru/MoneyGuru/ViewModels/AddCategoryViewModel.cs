using MoneyGuru.ViewModels;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace MoneyGuru
{
    public class AddCategoryViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}