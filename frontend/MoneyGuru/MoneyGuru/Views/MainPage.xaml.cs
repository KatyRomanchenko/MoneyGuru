using MoneyGuru.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MoneyGuru
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private async void OnAddTransactionClicked(object sender, EventArgs e)
        {
            bool parseSuccess = Enum.TryParse(TransactionTypePicker.SelectedItem.ToString(), out TransactionType transactionType);
            if (!parseSuccess)
            {
                await DisplayAlert("Error", "Invalid transaction type selected", "OK");
                return;
            }

            var newTransaction = new Transaction
            {
                //UserID = 123,
                TransactionType = transactionType,
                Date = TransactionDatePicker.Date,
                Amount = decimal.Parse(AmountEntry.Text),
                Category = CategoryPicker.SelectedItem.ToString()
            };

            //Database.db.Insert(newTransaction);

            await DisplayAlert("Success", "Transaction added successfully", "OK");
        }

        private async void OnAddCategoryClicked(object sender, EventArgs e)
        {
            string newCategory = await DisplayPromptAsync("New Category", "Enter new category name");
            if (!string.IsNullOrEmpty(newCategory))
            {
                var categories = (List<string>)CategoryPicker.ItemsSource;
                categories.Add(newCategory);

                // TODO: Saivg to database
            }
        }

        private async void OnLogOutClicked(object sender, EventArgs e)
        {
            //LogOut from this account
            Application.Current.MainPage = new PrestartPage();
        }
        private async void OnCurrencyCalculationClicked(object sender, EventArgs e)
        {
            //GOTO CurrencyCalc
            //await Navigation.PushAsync(new CurrencyCalculationPage());
            Application.Current.MainPage = new CurrencyCalculationPage();

        }

        private async void OnCalculationClicked(object sender, EventArgs e)
        {
            //LogOut from this account
            //Application.Current.MainPage = new CalculationPage();
        }
        private async void OnCreateGoalClicked(object sender, EventArgs e)
        {
            //LogOut from this account
            //Application.Current.MainPage = new CreateGoalPage();
        }
    }
}
