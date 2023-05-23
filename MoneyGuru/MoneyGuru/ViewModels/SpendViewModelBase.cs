﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace MoneyGuru
{
    public class SpendViewModelBase : INotifyPropertyChanged
    {

        private object _selectedItem;

        private ObservableCollection<TransactionDetail> _transactions;

        private ObservableCollection<ExpenseCategory> _categories;

        protected readonly INavigationService NavigationService;
        public AddTransactionDetail SingleTransaction { get; set; }
        public List<MainPageItem> MainPageItems { get; set; }

        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                if (_selectedItem != null)
                    NavigationService.NavigateToPage(_selectedItem as MainPageItem);
                NotifyPropertyChanged("SelectedItem");
            }
        }

        /// <summary>
        /// Gets or sets the overall transaction details 
        /// </summary>
        public ObservableCollection<TransactionDetail> Transactions
        {
            get
            {
                return _transactions;// ?? (_transactions = App.DataService.GetTransactions());
            }
            set
            {
                _transactions = value;
                NotifyPropertyChanged("Transactions");
            }
        }

        /// <summary>
        /// Gets or sets the overall transaction and category details
        /// </summary>
        public virtual ObservableCollection<ExpenseCategory> Categories
        {
            get
            {
                //if (_categories == null)
                //    _categories = App.DataService.GetCategories();
                var totalSpentOnCategory = Transactions.GroupBy(i => i.Category)
                    .Select(g => new { Category = g.Key, Value = g.Sum(i => i.Spent) }).ToList();

                for (var i = 0; i < _categories.Count; i++)
                {
                    _categories[i].Balance = _categories[i].Budget - totalSpentOnCategory[i].Value;
                    _categories[i].Percentage = totalSpentOnCategory[i].Value / _categories[i].Budget * 100;
                    _categories[i].Spent = totalSpentOnCategory[i].Value;
                    _categories[i].Transactions = Transactions.Where(j => j.Category == _categories[i].Name).ToList();
                }

                return _categories;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public SpendViewModelBase()
        {
            NavigationService = new NavigationService();
            MainPageItems = new List<MainPageItem>
            {
                new MainPageItem
                {
                    Title = "Overview",
                    IconSource = "user.png",
                    //TargetType = typeof (OverviewPage)
                },
                new MainPageItem
                {
                    Title = "Transaction",
                    IconSource = "message.png",
                    //TargetType = typeof (TransactionPage)
                },
                new MainPageItem
                {
                    Title = "Budget",
                    IconSource = "category.png",
                    //TargetType = typeof (CategoryBudgetPage)
                },
                new MainPageItem
                {
                    Title = "Trends",
                    IconSource = "trend.png",
                    //TargetType = typeof (TrendsPage)
                }
            };
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}