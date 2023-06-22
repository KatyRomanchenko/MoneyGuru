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
using System.Threading.Tasks;

namespace MoneyGuru
{
    public class TransactionViewModel : INotifyPropertyChanged
    {
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


        private string walletName1;
        public string WalletName1
        {
            get { return walletName1; }
            set
            {
                if (walletName1 != value)
                {
                    walletName1 = value;
                    OnPropertyChanged();
                }
            }
        }

        private string walletName2;
        public string WalletName2
        {
            get { return walletName2; }
            set
            {
                if (walletName2 != value)
                {
                    walletName2 = value;
                    OnPropertyChanged();
                }
            }
        }
        private string walletName3;
        public string WalletName3
        {
            get { return walletName3; }
            set
            {
                if (walletName3 != value)
                {
                    walletName3 = value;
                    OnPropertyChanged();
                }
            }
        }
        private string walletName4;
        public string WalletName4
        {
            get { return walletName4; }
            set
            {
                if (walletName4 != value)
                {
                    walletName4 = value;
                    OnPropertyChanged();
                }
            }
        }

        private string wallet1;
        public string Wallet1
        {
            get { return wallet1; }
            set
            {
                if (wallet1 != value)
                {
                    wallet1 = value;
                    OnPropertyChanged();
                }
            }
        }

        private string wallet2;
        public string Wallet2
        {
            get { return wallet2; }
            set
            {
                if (wallet2 != value)
                {
                    wallet2 = value;
                    OnPropertyChanged();
                }
            }
        }
        private string wallet3;
        public string Wallet3
        {
            get { return wallet3; }
            set
            {
                if (wallet3 != value)
                {
                    wallet3 = value;
                    OnPropertyChanged();
                }
            }
        }
        private string wallet4;
        public string Wallet4
        {
            get { return wallet4; }
            set
            {
                if (wallet4 != value)
                {
                    wallet4 = value;
                    OnPropertyChanged();
                }
            }
        }

        private string color1;
        public string Color1
        {
            get { return color1; }
            set
            {
                if (color1 != value)
                {
                    color1 = value;
                    OnPropertyChanged();
                }
            }
        }

        private string color2;
        public string Color2
        {
            get { return color2; }
            set
            {
                if (color2 != value)
                {
                    color2 = value;
                    OnPropertyChanged();
                }
            }
        }
        private string color3;
        public string Color3
        {
            get { return color3; }
            set
            {
                if (color3 != value)
                {
                    color3 = value;
                    OnPropertyChanged();
                }
            }
        }
        private string color4;
        public string Color4
        {
            get { return color4; }
            set
            {
                if (color4 != value)
                {
                    color4 = value;
                    OnPropertyChanged();
                }
            }
        }
        //public ObservableCollection<WalletData> Wallets { get; set; }

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
            GetWallet1();
            GetWallet2();
            GetWallet3();
            GetWallet4();


            //Wallets = new ObservableCollection<WalletData>();
            //LoadWallets();


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
                        transaction.Color = "#808080";
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

                //foreach (var wallet in wallets)
                //{
                    //Wallets.Add(wallet);
                //}
            }
        }

        public async Task GetWallet1()
        {
            HttpClientFactory httpClientFactory = new HttpClientFactory();
            HttpClient client = httpClientFactory.CreateAuthenticatedClient();

            var uri = new Uri(httpClientFactory.mainURL + "/api/wallet/formainpage");
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var walletData = await response.Content.ReadAsStringAsync();
                var wallets = JsonConvert.DeserializeObject<List<WalletData>>(walletData);

                if (wallets.Count < 1)
                {
                    WalletName1 = "";
                    Wallet1 = "";
                    Color1 = "#7853FA";
                }
                else
                {
                    WalletName1 = wallets[0].WalletName;
                    Wallet1 = wallets[0].WalletAmount.ToString();
                    Color1 = "#eeeeee";
                }
            }
        }
        public async Task GetWallet2()
        {
            HttpClientFactory httpClientFactory = new HttpClientFactory();
            HttpClient client = httpClientFactory.CreateAuthenticatedClient();

            var uri = new Uri(httpClientFactory.mainURL + "/api/wallet/formainpage");
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var walletData = await response.Content.ReadAsStringAsync();
                var wallets = JsonConvert.DeserializeObject<List<WalletData>>(walletData);

                if (wallets.Count < 2)
                {
                    WalletName2 = "";
                    Wallet2 = "";
                    Color2 = "#7853FA";
                }
                else
                {
                    WalletName2 = wallets[1].WalletName;
                    Wallet2 = wallets[1].WalletAmount.ToString();
                    Color2 = "#eeeeee";
                }
            }
        }
        public async Task GetWallet3()
        {
            HttpClientFactory httpClientFactory = new HttpClientFactory();
            HttpClient client = httpClientFactory.CreateAuthenticatedClient();

            var uri = new Uri(httpClientFactory.mainURL + "/api/wallet/formainpage");
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var walletData = await response.Content.ReadAsStringAsync();
                var wallets = JsonConvert.DeserializeObject<List<WalletData>>(walletData);

                if (wallets.Count < 3)
                {
                    WalletName3 = "";
                    Wallet3 = "";
                    Color3 = "#7853FA";
                }
                else
                {
                    WalletName3 = wallets[2].WalletName;
                    Wallet3 = wallets[2].WalletAmount.ToString();
                    Color3 = "#eeeeee";
                }
            }
        }
        public async Task GetWallet4()
        {
            HttpClientFactory httpClientFactory = new HttpClientFactory();
            HttpClient client = httpClientFactory.CreateAuthenticatedClient();

            var uri = new Uri(httpClientFactory.mainURL + "/api/wallet/formainpage");
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var walletData = await response.Content.ReadAsStringAsync();
                var wallets = JsonConvert.DeserializeObject<List<WalletData>>(walletData);

                if (wallets.Count < 4)
                {
                    WalletName4 = "";
                    Wallet4 = "";
                    Color4 = "#7853FA";
                }
                else
                {
                    WalletName4 = wallets[3].WalletName;
                    Wallet4 = wallets[3].WalletAmount.ToString();
                    Color4 = "#eeeeee";

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