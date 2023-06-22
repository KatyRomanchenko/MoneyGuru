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
using MoneyGuru;
using MoneyGuru.Services;
using Microcharts;
using SkiaSharp;
using System.Collections.Generic;

namespace MoneyGuru
{
    public partial class AddCategoryPage
    {
        public AddCategoryPage()
        {
            InitializeComponent();
        }
        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            HttpClientFactory httpClientFactory = new HttpClientFactory();
            HttpClient client = httpClientFactory.CreateAuthenticatedClient();

            var newCategory = new AddCategoryViewModel
            {
                Name = CategoryEntry.Text
            };

            var jsonData = JsonConvert.SerializeObject(newCategory);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(httpClientFactory.mainURL + "/api/category", content);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Success", "Category added successfully", "OK");
                Application.Current.MainPage = new NavigationPage(new MainPage())
                {
                    BarBackgroundColor = Color.FromHex("#7853FA"),
                    BarTextColor = Color.Black
                };
            }
            else
            {
                var messageEx = "Something went wrong";
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