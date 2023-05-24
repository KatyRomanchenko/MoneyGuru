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
            Application.Current.MainPage = new LoginEntryPage();
        }

        private void OnSignUpClicked(object sender, EventArgs e)
        {
             Application.Current.MainPage = new SignInPage();
        }
    }
}