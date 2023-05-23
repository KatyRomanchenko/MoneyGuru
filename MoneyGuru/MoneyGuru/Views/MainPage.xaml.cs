using MoneyGuru.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MoneyGuru
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }
    }
}
