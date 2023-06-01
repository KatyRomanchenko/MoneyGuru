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
using System.Windows.Input;
using Xamarin.Forms;

namespace MoneyGuru
{
    public class TransactionViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Transaction> transactions;

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

            var uri = new Uri("http://192.168.1.6:5000/api/transaction");
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
}