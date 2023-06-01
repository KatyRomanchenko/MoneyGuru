using System;
using Xamarin.Forms;

namespace MoneyGuru
{
    public partial class PrestartPage
    {
        public PrestartPage()
        {
            InitializeComponent();
        }

        private async void OnLogInClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new LoginEntryPage())
            {
                BarBackgroundColor = Color.FromHex("#7853FA"),
                BarTextColor = Color.Black
            };
        }

        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new SignInPage())
            {
                BarBackgroundColor = Color.FromHex("#7853FA"),
                BarTextColor = Color.Black
            };
        }
    }
}