using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace MoneyGuru.ViewModels
{
    public class CreateWalletViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _walletName;
        private string _selectedIcon;
        private decimal _walletAmount;

        public string WalletName
        {
            get => _walletName;
            set
            {
                _walletName = value;
                OnPropertyChanged();
            }
        }

        public string SelectedIcon
        {
            get => _selectedIcon;
            set
            {
                _selectedIcon = value;
                OnPropertyChanged();
            }
        }

        public decimal WalletAmount
        {
            get => _walletAmount;
            set
            {
                _walletAmount = value;
                OnPropertyChanged();
            }
        }

        public List<string> IconList { get; set; }

        public ICommand CreateWalletCommand { get; set; }

        public CreateWalletViewModel()
        {
            // Populate IconList with icons. Replace this with your actual list of icons.
            IconList = new List<string> { "Icon1", "Icon2", "Icon3" };

            // Initialize CreateWalletCommand
            CreateWalletCommand = new Command(CreateWallet);
        }

        private void CreateWallet()
        {
            // Add your create wallet logic here.
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}