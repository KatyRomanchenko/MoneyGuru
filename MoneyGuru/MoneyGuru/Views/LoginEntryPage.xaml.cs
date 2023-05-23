using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System;

namespace MoneyGuru
{
    public partial class LoginEntryPage
    {
        private string _email;
        private string _password;
        private UserService _userService;
        private bool _isLoginEnabled;
        public LoginEntryPage()
        {
            InitializeComponent();
            //_userService = new UserService();
            //LoginCommand = new Command(OnLoginClicked, CanLogin);
        }
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
                ((Command)LoginCommand).ChangeCanExecute();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
                ((Command)LoginCommand).ChangeCanExecute();
            }
        }

        public bool IsLoginEnabled
        {
            get => _isLoginEnabled;
            set
            {
                _isLoginEnabled = value;
                OnPropertyChanged(nameof(IsLoginEnabled));
            }
        }
        public ICommand LoginCommand { get; }
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            //if (_userService.Login(Email, Password))
            //{
                Application.Current.MainPage = new MainPage(); 
            //}
            //else
            //{
                // Show an error message
            //    await Application.Current.MainPage.DisplayAlert("Error", "Invalid login attempt", "OK");
            //}
        }

        private bool CanLogin()
        {
            bool emailIsValid = !string.IsNullOrEmpty(Email);
            bool passwordIsValid = !string.IsNullOrEmpty(Password);
            IsLoginEnabled = emailIsValid && passwordIsValid;

            return IsLoginEnabled;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public class UserService
        {
            public bool Login(string email, string password)
            {
                // In real-world app, you should be fetching user and comparing hashed and salted password. 
                // This is just a basic example and should not be used for actual authentication.
                return email == "email" && password == "password";
            }
        }
    }
}