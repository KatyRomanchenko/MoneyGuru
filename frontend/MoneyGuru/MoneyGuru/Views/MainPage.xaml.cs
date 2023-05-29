using MoneyGuru.Services;
using MoneyGuru.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Authenticators.OAuth2;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MoneyGuru
{
    public partial class MainPage
    {
        public TransactionViewModel TransactionViewModel { get; set; }

        public MainPage()
        {
            InitializeComponent();
            BarBackgroundColor = Color.FromHex("#E9C31E");
            TransactionViewModel = new TransactionViewModel();
            BindingContext = TransactionViewModel;
        }

        private async void OnAddTransactionClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new AddTransactionPage());
        }

        private async void OnAddCategoryClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new AddCategoryPage());
        }

        private async void OnAddWalletClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new AddWalletPage());
        }

        private async void OnAddIncomeClicked(object sender, EventArgs e)
        {
            //Application.Current.MainPage = new NavigationPage(new AddTransactionPage());
        }
        private async void OnCreateGoalClicked(object sender, EventArgs e)// To add GOALS logic
        {
            //Application.Current.MainPage = new CreateGoalPage();
        }

        private async void OnLogOutClicked(object sender, EventArgs e)
        {
            //LogOut from this account
            Application.Current.MainPage = new NavigationPage(new PrestartPage())
            {
                BarBackgroundColor = Color.FromHex("#7853FA"),
                BarTextColor = Color.Black
            };
        }

        private async void OnCurrencyCalculationClicked(object sender, EventArgs e)
        {
            //GOTO CurrencyCalc
            //await Navigation.PushAsync(new CurrencyCalculationPage());
            Application.Current.MainPage = new CurrencyCalculationPage();

        }
        private async void OnCalculationClicked(object sender, EventArgs e)
        {
            //Application.Current.MainPage = new CalculationPage();
        }
    }
}
