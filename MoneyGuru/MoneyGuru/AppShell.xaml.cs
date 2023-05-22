using MoneyGuru.ViewModels;
using MoneyGuru.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MoneyGuru
{
    public partial class AppShell : ContentPage
    {
        public AppShell()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//AboutPage");
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
