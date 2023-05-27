using MoneyGuru.ViewModels;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace MoneyGuru
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Total wallet amount property
        private decimal _totalWalletAmount;
        public decimal TotalWalletAmount
        {
            get => _totalWalletAmount;
            set
            {
                _totalWalletAmount = value;
                OnPropertyChanged();
            }
        }

        // Monthly savings amount property
        private decimal _monthlySavingsAmount;
        public decimal MonthlySavingsAmount
        {
            get => _monthlySavingsAmount;
            set
            {
                _monthlySavingsAmount = value;
                OnPropertyChanged();
            }
        }

        // Command for creating a wallet
        public ICommand CreateWalletCommand { get; set; }

        public MainViewModel()
        {
            // Initialize CreateWalletCommand with some command
            // This could be creating a new page, opening a dialog, etc.
            CreateWalletCommand = new Command(async () => 
            {
                //await Application.Current.MainPage.Navigation.PushAsync(new Views.CreateWalletPage());
                await Shell.Current.Navigation.PushAsync(new Views.CreateWalletPage());

            });
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }


}