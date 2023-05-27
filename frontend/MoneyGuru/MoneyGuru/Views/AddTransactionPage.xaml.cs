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
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };

            HttpClient client = new HttpClient(handler);

            var newTransaction = new AddTransactionViewModel
            {
                Email = EmailEntry.Text,
                Password = PasswordEntry.Text,
                ConfirmPassword = ConfirmPasswordEntry.Text
            };

            var jsonData = JsonConvert.SerializeObject(newTransaction);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://192.168.1.3:5000/api/auth/Register", content);


            //await DisplayAlert("ALARM", ex.Message, "OK");

            var responseBody = await response.Content.ReadAsStringAsync();

            var responseObject = JsonConvert.DeserializeObject<UserManagerResponse>(responseBody);

            if (responseObject.IsSuccess)
            {
                await DisplayAlert("Success", "User created successfully", "OK");
                Application.Current.MainPage = new LoginEntryPage();
            }
            else
            {
                var message = responseObject.Message;
                await DisplayAlert("Error", message, "OK");
            }
        }
    }
}