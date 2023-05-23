using MoneyGuru.ViewModels;
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

        private void OnLogInClicked(object sender, EventArgs e)
        {
            // Implement your navigation to Login page here.
            // For example:
            Application.Current.MainPage = new LoginEntryPage();
            // Application.Current.MainPage = new LoginPage();
        }

        private void OnSignUpClicked(object sender, EventArgs e)
        {
            // Implement your navigation to Sign up page here.
            // For example:
             Application.Current.MainPage = new LoginEntryPage();
            //await Application.Current.MainPage.Navigation.PushAsync(new LoginEntryPage());

        }
    }
}