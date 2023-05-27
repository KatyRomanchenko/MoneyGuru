using MoneyGuru.ViewModels;
using MoneyGuru.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            await Navigation.PushAsync(new LoginEntryPage());
        }

        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignInPage());
        }
    }
}