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
        public string Total { get; set; }
        public string TotalCollected { get; set; }
        public string TotalEarned { get; set; }
        public string TotalSaved { get; set; }
        public string TotalWallets { get; set; }
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

            Data = new ObservableCollection<Model>()
            {
            new Model("Jan", 50),
            new Model("Feb", 70),
            new Model("Mar", 65),
            new Model("Apr", 57),
            new Model("May", 48),
            };
            Total = "1435";
            TotalCollected = "500";
            TotalEarned = "1500";
            TotalSaved = "1000";
            TotalWallets = "435";
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
                        transaction.Amount = -Math.Abs(transaction.Amount);  // Ensure the amount is negative
                    }
                    else if (transaction.TransactionType == "Income")
                    {
                        transaction.Amount = Math.Abs(transaction.Amount);  // Ensure the amount is positive
                        transaction.Category = "Income";
                    }
                }

                Transactions = new ObservableCollection<Transaction>(transactionsList);
            }
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
}