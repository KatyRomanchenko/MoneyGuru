using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System;
using System.Linq;
using MoneyGuru.ViewModels;

namespace MoneyGuru
{
    public partial class SignInPage : ContentPage
    {
        public SignInPage()
        {
            InitializeComponent();
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            var existingUser = Database.db.Table<User>().FirstOrDefault(x => x.Email == EmailEntry.Text);
            if (existingUser == null)
            {
                // Register the user here
                var newUser = new User
                {
                    //UserID = GenerateUserID(),
                    Email = EmailEntry.Text,
                    Password = PasswordEntry.Text,
                    Token = Guid.NewGuid().ToString(), //Generating a new GUID as a token

                };
                Database.db.Insert(newUser);
                await DisplayAlert("Success", "User created successfully", "OK");
                Application.Current.MainPage = new LoginEntryPage();
                //await Navigation.PopAsync(); //go back to the login page
            }
            else
            {
                await DisplayAlert("Error", "User with this email already exists", "OK");
            }
        }

        private static readonly Random random = new Random();
    }
}