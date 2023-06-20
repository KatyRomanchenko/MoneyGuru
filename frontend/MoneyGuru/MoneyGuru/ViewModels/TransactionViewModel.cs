using MoneyGuru.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace MoneyGuru
{
    public class TransactionViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Transaction> transactions;
        public ObservableCollection<Model> Data { get; set; }
        public ObservableCollection<Model1> Data1 { get; set; }

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


        public string Wallet1 { get; set; }
        public string Wallet2 { get; set; }
        public string Wallet3 { get; set; }
        public string Wallet4 { get; set; }

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

            //Data = new ObservableCollection<Model>()
            //{
            //new Model1("May", 48),
            //new Model1("June", 155)
            //};
            Data1 = new ObservableCollection<Model1>()
            {
                new Model1("Category1", 50),
                new Model1("Category2", 70),
                new Model1("Category3", 65),
                new Model1("Category4", 57),
                new Model1("Category5", 48),
            };

            Data = new ObservableCollection<Model>()
        {
            new Model("Jan", 50),
            new Model("Feb", 70),
            new Model("Mar", 65),
            new Model("Apr", 57),
            new Model("May", 48),
        };

            Wallet1 = "435";
            Wallet2 = "";
            Wallet3 = "";
            Wallet4 = "";
        }

        public event PropertyChangedEventHandler PropertyChanged;
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
            public string Category { get; set; }
            public double Value { get; set; }

            public Model1(string category, double value)
            {
                Category = category;
                Value = value;
            }
        }
    }
}