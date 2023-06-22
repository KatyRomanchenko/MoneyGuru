using MoneyGuru.Services;
using Newtonsoft.Json;
using Xamarin.Forms;
using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Linq;
using Newtonsoft.Json.Linq;
using Microcharts;
using SkiaSharp;
using DevExpress.Data.Helpers;

namespace MoneyGuru
{
    public class TransactionViewModel : INotifyPropertyChanged
    {
        public event Action NeedRefresh;
        public ObservableCollection<Color> CustomPalette { get; set; }

        private ObservableCollection<Transaction> transactions;
        public ObservableCollection<Model> Data { get; set; }
        public ObservableCollection<Model1> Data1 { get; set; }
        public ObservableCollection<DataModel> Data3 { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<AnalysisModel> _analysisData;
        public ObservableCollection<AnalysisModel> AnalysisData
        {
            get => _analysisData;
            set
            {
                if (_analysisData != value)
                {
                    _analysisData = value;
                    OnPropertyChanged();
                }
            }
        }

        private string total;
        public string Total
        {
            get { return total; }
            set
            {
                if (total != value)
                {
                    total = value;
                    OnPropertyChanged();
                }
            }
        }

        private string spentThisMonth;
        public string SpentThisMonth
        {
            get { return spentThisMonth; }
            set
            {
                if (spentThisMonth != value)
                {
                    spentThisMonth = value;
                    OnPropertyChanged();
                }
            }
        }

        private string totalEarned;
        public string TotalEarned
        {
            get { return totalEarned; }
            set
            {
                if (totalEarned != value)
                {
                    totalEarned = value;
                    OnPropertyChanged();
                }
            }
        }

        private string totalSaved;
        public string TotalSaved
        {
            get { return totalSaved; }
            set
            {
                if (totalSaved != value)
                {
                    totalSaved = value;
                    OnPropertyChanged();
                }
            }
        }

        private string totalWallets;
        public string TotalWallets
        {
            get { return totalWallets; }
            set
            {
                if (totalWallets != value)
                {
                    totalWallets = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _centerText;
        public string CenterText
        {
            get => _centerText;
            set
            {
                _centerText = value;
                OnPropertyChanged(nameof(CenterText));
            }
        }
        private string maxValue;
        public string MaxValue
        {
            get { return maxValue; }
            set
            {
                if (maxValue != value)
                {
                    maxValue = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<WalletData> Wallets { get; set; }

        //public string Wallet1 { get; set; }
        //public string Wallet2 { get; set; }
        //public string Wallet3 { get; set; }
        //public string Wallet4 { get; set; }

        public ObservableCollection<Transaction> Transactions
        {
            get { return transactions; }
            set
            {
                if (transactions != value)
                {
                    transactions = value;
                    OnPropertyChanged();
                }
            }
        }

        public TransactionViewModel()
        {
            GetTransactions();
            GetTotal();
            GetTotalEarned();
            GetTotalWallets();
            GetTotalSaved();
            GetSpentThisMonth();
            FetchDataAsync();

            Wallets = new ObservableCollection<WalletData>();
            LoadWallets();


            Data1 = new ObservableCollection<Model1>()
            {
                new Model1(2001, 50),
                new Model1(2002, 70),
                new Model1(2003, 65),
                new Model1(2003, 57),
                new Model1(2010, 48),
            };

            CenterText = "75%";
            Data = new ObservableCollection<Model>()
        {
            new Model("sEP", 200),
            new Model("Sweets", 100)
        };
            Data3 = new ObservableCollection<DataModel>() 
            {
                new DataModel("13/11/2023", 200),
                new DataModel("13/11/2023", 200),
                new DataModel("14/11/2023", 800)
            };

            //Wallet1 = "435";
           // Wallet2 = "";
            //Wallet3 = "";
           // Wallet4 = "";

            CustomPalette = new ObservableCollection<Color>
    {
        Color.FromHex("#372774"),
        Color.FromHex("#E9C31E"),
    };
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public async void GetTransactions()
        {
            HttpClientFactory httpClientFactory = new HttpClientFactory();
            HttpClient client = httpClientFactory.CreateAuthenticatedClient();

            var uri = new Uri(httpClientFactory.mainURL + "/api/transaction");
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
                var transactionsList = JsonConvert.DeserializeObject<List<Transaction>>(content, settings);

                foreach (var transaction in transactionsList)
                {
                    if (transaction.TransactionType == "Expense")
                    {
                        transaction.Amount = -Math.Abs(transaction.Amount);
                        transaction.Color = "#808080"; // Assuming you want a different color for "Expense", replace "#FF0000" with your desired color.
                    }
                    else if (transaction.TransactionType == "Income")
                    {
                        transaction.Amount = Math.Abs(transaction.Amount);
                        transaction.Category = "Income";
                        transaction.Color = "#7853FA";
                    }
                }

                Transactions = new ObservableCollection<Transaction>(transactionsList);
            }
        }
        public async void GetTotal()
        {
            HttpClientFactory httpClientFactory = new HttpClientFactory();
            HttpClient client = httpClientFactory.CreateAuthenticatedClient();

            var uri = new Uri(httpClientFactory.mainURL + "/api/wallet/total");
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                if (content == "" || content == "0")
                {
                    Total = "0";
                }
                else
                {
                    Total = content;
                }
            }
        }
        public async void GetTotalEarned()
        {
            HttpClientFactory httpClientFactory = new HttpClientFactory();
            HttpClient client = httpClientFactory.CreateAuthenticatedClient();

            var uri = new Uri(httpClientFactory.mainURL + "/api/transaction/totalEarned");
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                if (content == "" || content == "0")
                {
                    TotalEarned = "0";
                }
                else
                {
                    TotalEarned = content; ;
                };
            }
        }
        public async void GetTotalWallets()
        {
            HttpClientFactory httpClientFactory = new HttpClientFactory();
            HttpClient client = httpClientFactory.CreateAuthenticatedClient();

            var uri = new Uri(httpClientFactory.mainURL + "/api/wallet/totalwallets");
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                if (content == "" || content == "0")
                {
                    TotalWallets = "0";
                }

                else
                {
                    TotalWallets = content;
                    MaxValue = content;
                };
            }
        }
        public async void GetTotalSaved()
        {
            HttpClientFactory httpClientFactory = new HttpClientFactory();
            HttpClient client = httpClientFactory.CreateAuthenticatedClient();

            var uri = new Uri(httpClientFactory.mainURL + "/api/wallet/totalsaved");
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                if (content == "" || content == "0")
                {
                    TotalSaved = "0";
                }

                else
                {
                    TotalSaved = content;
                };
            }
        }
        public async void GetSpentThisMonth()
        {
            HttpClientFactory httpClientFactory = new HttpClientFactory();
            HttpClient client = httpClientFactory.CreateAuthenticatedClient();

            var uri = new Uri(httpClientFactory.mainURL + "/api/transaction/totalSpent");
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                if (content == "" || content == "0")
                {
                    SpentThisMonth = "0";
                }

                else
                {
                    SpentThisMonth = content;
                };
            }
        }
        private async void FetchDataAsync()
        {
            HttpClientFactory httpClientFactory = new HttpClientFactory();
            HttpClient client = httpClientFactory.CreateAuthenticatedClient();

            var uri = new Uri(httpClientFactory.mainURL + "/api/transaction/totalSpentByCategory");
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
                var dataList = JsonConvert.DeserializeObject<List<AnalysisModel>>(content, settings);

                var sortedDataList = dataList.OrderBy(x => x.TotalSpent).ToList();

                var newAnalysisData = new ObservableCollection<AnalysisModel>();
                foreach (var data in sortedDataList)
                {
                    newAnalysisData.Add(new AnalysisModel(data.Category, data.TotalSpent));
                }

                AnalysisData = newAnalysisData;
            }
        }
        private async void LoadWallets()
        {
            HttpClientFactory httpClientFactory = new HttpClientFactory();
            HttpClient client = httpClientFactory.CreateAuthenticatedClient();

            var uri = new Uri(httpClientFactory.mainURL + "/api/wallet/details");
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var wallets = JsonConvert.DeserializeObject<List<WalletData>>(content);

                foreach (var wallet in wallets)
                {
                    Wallets.Add(wallet);
                }
            }
        }
        public class AnalysisModel
        {
            public string Category { get; set; }
            public double TotalSpent { get; set; }
            public AnalysisModel(string xValue, double yValue)
            {
                Category = xValue;
                TotalSpent = yValue;
            }
        }

        public class Model
        {
            public string Month { get; set; }
            public double Target { get; set; }

            public Model(string xValue, double yValue)
            {
                Month = xValue;
                Target = yValue;
            }
        }

        public class Model1
        {
            public int Year { get; set; }
            public double Value { get; set; }

            public Model1(int xValue, double yValue)
            {
                Year = xValue;
                Value = yValue;
            }
        }

        public class DataModel
        {
            public string Day { get; set; }
            public double Value { get; set; }
            public DataModel(string xValue, double yValue)
            {
                Day = xValue;
                Value = yValue;
            }
        }
        public class WalletData
        {
            public string WalletName { get; set; }
            public double WalletAmount { get; set; }
        }
    }
}