using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System;
using System.Linq;
using Xamarin.Essentials;
using System.Net.Http;
using MoneyGuru;
using MoneyGuru.ViewModels;
using Newtonsoft.Json;
using System.Text;

namespace MoneyGuru
{
    public partial class LoginEntryPage 
    { 
        public LoginEntryPage()
        {
            InitializeComponent();
        }
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            //DIRTYHACK. Remove before production

            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };

            HttpClient client = new HttpClient(handler);

            var model = new LoginEntryViewModel
            {
                Email = EmailEntry.Text,
                Password = PasswordEntry.Text,
            };

            var jsonData = JsonConvert.SerializeObject(model);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://192.168.1.3:5000/api/auth/Login", content);

            var responseBody = await response.Content.ReadAsStringAsync();

            var responseObject = JsonConvert.DeserializeObject<UserManagerResponse>(responseBody);

            if (responseObject.IsSuccess)
            {
                // Frame.Navigate(typeof(MainPage), responseObject.Message);
                Application.Current.MainPage = new MainPage();
            }
            else
            {
                var message = responseObject.Message;
                await DisplayAlert("Success", message, "OK");

            }
        }
    }
}