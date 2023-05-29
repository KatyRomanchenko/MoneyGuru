using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System;
using System.Linq;
using MoneyGuru.ViewModels;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using MoneyGuru.Services;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MoneyGuru
{
    public partial class AddTransactionPage
    {
        public AddTransactionPage()
        {
            InitializeComponent();
        }
        private async void OnAddTransactionClicked(object sender, EventArgs e)
        {
            HttpClientFactory httpClientFactory = new HttpClientFactory();
            HttpClient client = httpClientFactory.CreateAuthenticatedClient();

            decimal amount = Convert.ToDecimal(AmountEntry.Text);
            var newTransaction = new AddTransactionViewModel
            {
                TransactionType = "Income",
                TransactionName = EntryName.Text,
                Amount = amount,
                Date = TransactionDatePicker.Date,
                //Category = CategoryPicker.SelectedItem.ToString(),
                Wallet = WalletPicker.SelectedItem.ToString(),
            };
            var jsonData = JsonConvert.SerializeObject(newTransaction);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://192.168.1.3:5000/api/transaction", content);

            if (response.IsSuccessStatusCode)
            {
                // HERE SYSTEM SHOULD FING and GET "AmountOfWallet" from API by "Wallet " name AND DO: newAmountOfMoney = "AmountOfMoney"(From API) - amount (from newTransaction), and PUT newAmountOfMoney instead "AmountOfMoney"(From API)
                await DisplayAlert("Success", "Transaction added successfully", "OK");
            }
            else
            {
                var messageEx = "Error occurred";
                await DisplayAlert("Error", messageEx, "OK");
                return;
            }
        }
        private async void OnGoBackClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.FromHex("#7853FA"),
                BarTextColor = Color.Black
            };
            Application.Current.MainPage.Title = "MAIN DASHBOARD";
        }
    }
}
