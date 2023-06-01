using MoneyGuru.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace MoneyGuru
{
    public class WalletViewModel : INotifyPropertyChanged
    {
        private List<string> _walletsNames;
        public List<string> WalletsNames
        {
            get { return _walletsNames; }
            set
            {
                _walletsNames = value;
                OnPropertyChanged("WalletsNames");
            }
        }

        public WalletViewModel()
        {
            GetWalletsNames();
        }

        public async void GetWalletsNames()
        {
            HttpClientFactory httpClientFactory = new HttpClientFactory();
            HttpClient client = httpClientFactory.CreateAuthenticatedClient();

            var uri = new Uri(httpClientFactory.mainURL + "/api/wallet");
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var walletsNames = JsonConvert.DeserializeObject<List<string>>(content);
                WalletsNames = walletsNames;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}