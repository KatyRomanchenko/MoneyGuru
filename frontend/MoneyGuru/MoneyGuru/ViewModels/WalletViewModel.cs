using MoneyGuru.Data;
using MoneyGuru.Services;
using MoneyGuru.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace MoneyGuru
{
    public class WalletViewModel : INotifyPropertyChanged
    {
        private List<string> _wallets;
        public List<string> Wallets
        {
            get { return _wallets; }
            set
            {
                _wallets = value;
                OnPropertyChanged("Wallets");
            }
        }

        public WalletViewModel()
        {
            GetWallets();
        }

        public async void GetWallets()
        {
            HttpClientFactory httpClientFactory = new HttpClientFactory();
            HttpClient client = httpClientFactory.CreateAuthenticatedClient();

            var uri = new Uri("http://192.168.1.3:5000/api/wallet");
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var wallets = JsonConvert.DeserializeObject<List<string>>(content);
                Wallets = wallets;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}