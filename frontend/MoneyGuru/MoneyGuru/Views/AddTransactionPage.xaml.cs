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
using MoneyGuru.Views;
using MoneyGuru.Services;

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
                Category = CategoryPicker.SelectedItem.ToString(),
                Wallet = "Cash"
            };

            var jsonData = JsonConvert.SerializeObject(newTransaction);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://192.168.1.3:5000/api/transaction", content);

            if (response.IsSuccessStatusCode)
            {
                 await DisplayAlert("Success", "Transaction added successfully", "OK");
            }
            else
            {
                //await DisplayAlert("Error", "Invalid transaction type selected", "OK");
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