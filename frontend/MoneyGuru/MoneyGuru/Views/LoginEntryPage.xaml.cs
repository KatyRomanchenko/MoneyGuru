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
using MoneyGuru.Services;

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
            HttpClientFactory httpClientFactory = new HttpClientFactory();
            HttpClient client = httpClientFactory.CreateAuthenticatedClient();

            var model = new LoginEntryViewModel
            {
                Email = EmailEntry.Text,
                Password = PasswordEntry.Text,
            };

            var jsonData = JsonConvert.SerializeObject(model);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://192.168.1.6:5000/api/auth/Login", content);

            var responseBody = await response.Content.ReadAsStringAsync();

            var responseObject = JsonConvert.DeserializeObject<UserManagerResponse>(responseBody);

            if (responseObject.IsSuccess)
            {
                Application.Current.MainPage = new MainPage();
            }
            else
            {
                var message = "";
                await DisplayAlert("Error", "Something went wrong", "OK");
            }
        }
        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new PrestartPage())
            {
                BarBackgroundColor = Color.FromHex("#7853FA"),
                BarTextColor = Color.Black
            };
        }
    }
}