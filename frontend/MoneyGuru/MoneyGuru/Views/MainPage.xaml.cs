using MoneyGuru.ViewModels;
using Newtonsoft.Json;
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
        private string _token;
        public MainPage(string token)
        {
            _token = token;
            InitializeComponent();
        }

        private async void OnAddTransactionClicked(object sender, EventArgs e)
        {
            decimal amount = Convert.ToDecimal(AmountEntry.Text);
            var newTransaction = new AddTransactionViewModel
            {
                TransactionType = "Income",
                Amount = amount,
                Date = TransactionDatePicker.Date,
                Category = CategoryPicker.ToString(),
            };
            //_token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJFbWFpbCI6ImthdHkucm9tYW5jaGVua29AZ21haWwuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiJjNDI4YTgzYy1kNmY4LTRlOGYtYmE4MC1hZWFiM2VmMDkwMDQiLCJleHAiOjE2ODc4NzQ3OTIsImlzcyI6Imh0dHA6Ly9haG1hZG1vemFmZmFyLm5ldCIsImF1ZCI6Imh0dHA6Ly9haG1hZG1vemFmZmFyLm5ldCJ9.ryFn8Rm3wKCO66TpdQloYxBy1qfmjl-MnCM7dNv4bdg";
            var isTrue = await GetAsync();
            var isSuccess = await SendAsync(newTransaction);
            //var isSuccess = await SendAsync(newTransaction);
            var isSecSucces = await SendRestAsync(newTransaction, _token);

            if (isSuccess)
            {
                await DisplayAlert("Success", "Transaction added successfully", "OK");
            }
            else
            {
                var messageEx = "Error occurred";
                await DisplayAlert("Error", messageEx, "OK");
            }
        }

        private async Task<bool> GetAsync()
        {
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
            HttpClient client = new HttpClient(handler);

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
            var response = await client.GetAsync("http://192.168.1.3:5000/api/transaction");

            var responseBody = await response.Content?.ReadAsStringAsync();
            return response.IsSuccessStatusCode;
        }

        private async Task<bool> SendRestAsync(AddTransactionViewModel newTransaction, string token)
        {
            var host = "http://192.168.1.3:5000";
            var options = new RestClientOptions(host)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var client = new RestClient(options);
            var request = new RestRequest("api/transaction", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {token}");
            request.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token, "Bearer");
            request.AddJsonBody(JsonConvert.SerializeObject(newTransaction));
            var result = await client.ExecuteAsync(request);
            return result.IsSuccessStatusCode;
        }

        private async Task<bool> SendAsync(AddTransactionViewModel newTransaction)
        {
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };

            _token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJFbWFpbCI6ImthdHkucm9tYW5jaGVua29AZ21haWwuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiJjNDI4YTgzYy1kNmY4LTRlOGYtYmE4MC1hZWFiM2VmMDkwMDQiLCJleHAiOjE2ODc4NzQ3OTIsImlzcyI6Imh0dHA6Ly9haG1hZG1vemFmZmFyLm5ldCIsImF1ZCI6Imh0dHA6Ly9haG1hZG1vemFmZmFyLm5ldCJ9.ryFn8Rm3wKCO66TpdQloYxBy1qfmjl-MnCM7dNv4bdg";
            HttpClient client = new HttpClient(handler);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var jsonData = JsonConvert.SerializeObject(newTransaction, Formatting.None, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var message = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                Content = content,
                RequestUri = new Uri("http://192.168.1.3:5000/api/transaction")
            };
            var response = await client.SendAsync(message);

            var responseBody = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<UserManagerResponse>(responseBody);

            return response.IsSuccessStatusCode;
        }

        private async void OnAddCategoryClicked(object sender, EventArgs e)
        {
            string newCategory = await DisplayPromptAsync("New Category", "Enter new category name");
            if (!string.IsNullOrEmpty(newCategory))
            {
                var categories = (List<string>)CategoryPicker.ItemsSource;
                categories.Add(newCategory);

                // TODO: Saivg to database
            }
        }

        private async void OnLogOutClicked(object sender, EventArgs e)
        {
            //LogOut from this account
            Application.Current.MainPage = new PrestartPage();
        }
        private async void OnCurrencyCalculationClicked(object sender, EventArgs e)
        {
            //GOTO CurrencyCalc
            //await Navigation.PushAsync(new CurrencyCalculationPage());
            Application.Current.MainPage = new CurrencyCalculationPage();

        }
        private async void OnCalculationClicked(object sender, EventArgs e)
        {
            //LogOut from this account
            //Application.Current.MainPage = new CalculationPage();
        }
        private async void OnCreateGoalClicked(object sender, EventArgs e)
        {
            //LogOut from this account
            //Application.Current.MainPage = new CreateGoalPage();
        }
        //private HttpClient CreateAuthenticatedClient()
        //{
        //    var handler = new HttpClientHandler();
        //    handler.ClientCertificateOptions = ClientCertificateOption.Manual;
        //    handler.ServerCertificateCustomValidationCallback =
        //        (httpRequestMessage, cert, cetChain, policyErrors) =>
        //        {
        //            return true;
        //        };

        //    HttpClient client = new HttpClient(handler);
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        //    return client;
        //}
    }
}
