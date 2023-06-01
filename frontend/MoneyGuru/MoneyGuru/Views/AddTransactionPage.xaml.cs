using Xamarin.Forms;
using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using MoneyGuru.Services;
using MoneyGuru.Data;

namespace MoneyGuru
{
    public partial class AddTransactionPage
    {
        public AddTransactionPage()
        {
            InitializeComponent();
        }
        public class UpdateWalletViewModel
        {
            public decimal AmountOfMoney { get; set; }
        }
        private async void OnAddTransactionClicked(object sender, EventArgs e)
        {
            HttpClientFactory httpClientFactory = new HttpClientFactory();
            HttpClient client = httpClientFactory.CreateAuthenticatedClient();

            decimal amount = Convert.ToDecimal(AmountEntry.Text);
            var newTransaction = new AddTransactionViewModel
            {
                TransactionType = "Expense",
                TransactionName = EntryName.Text,
                Amount = amount,
                Date = TransactionDatePicker.Date,
                Category = CategoryPicker.SelectedItem.ToString(),
                Wallet = WalletPicker.SelectedItem.ToString(),
            };

            var jsonData = JsonConvert.SerializeObject(newTransaction);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(httpClientFactory.mainURL + "/api/transaction", content);

            if (response.IsSuccessStatusCode)
            {
                var walletResponse = await client.GetAsync(httpClientFactory.mainURL + $"/api/wallet/{newTransaction.Wallet}");

                if (walletResponse.IsSuccessStatusCode)
                {
                    var walletData = await walletResponse.Content.ReadAsStringAsync();
                    var wallet = JsonConvert.DeserializeObject<Wallet>(walletData);

                    decimal newAmountOfMoney = wallet.AmountOfMoney - amount;

                    var updatedWallet = new Wallet
                    {
                        WalletName = wallet.WalletName,
                        AmountOfMoney = newAmountOfMoney
                    };

                    var updatedWalletJson = JsonConvert.SerializeObject(updatedWallet);
                    var walletUpdateContent = new StringContent(updatedWalletJson, Encoding.UTF8, "application/json");

                    var updateResponse = await client.PutAsync(httpClientFactory.mainURL + $"/api/wallet/{updatedWallet.WalletName}", walletUpdateContent);

                    if (updateResponse.IsSuccessStatusCode)
                    {
                        await DisplayAlert("Success", "Transaction added successfully", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Error", "Could not update wallet", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Error", "Could not fetch wallet", "OK");
                }
            }
            else
            {
                var errorMessage = "Error occurred";
                await DisplayAlert("Error", errorMessage, "OK");
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
