using MoneyGuru.Data;
using MoneyGuru.Services;
using MoneyGuru.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MoneyGuru.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _totalAmountString;

        public string TotalAmountString
        {
            get { return _totalAmountString; }
            set
            {
                if (_totalAmountString != value)
                {
                    _totalAmountString = value;
                    OnPropertyChanged();
                }
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainPageViewModel()
        {
            UpdateTotalAmountAsync();
        }

        private async Task UpdateTotalAmountAsync()
        {
            HttpClientFactory httpClientFactory = new HttpClientFactory();
            HttpClient client = httpClientFactory.CreateAuthenticatedClient();

            var response = await client.GetAsync(httpClientFactory.mainURL + "/api/wallet/totalamount");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                TotalAmountString = JsonConvert.DeserializeObject<decimal>(content).ToString();
            }
            else
            {
                TotalAmountString = "0";
            }
        }
    }
}