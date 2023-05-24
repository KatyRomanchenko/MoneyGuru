using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System;
using System.Linq;

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
            var existingUser = App.Database.Table<User>().FirstOrDefault(x => x.Email == EmailEntry.Text);
            if (existingUser == null)
            {
                // Register the user here
                var newUser = new User
                {
                    Email = EmailEntry.Text,
                    Password = PasswordEntry.Text,
                    Token = Guid.NewGuid().ToString(), //Generating a new GUID as a token

                };
                App.Database.Insert(newUser);
                await DisplayAlert("Success", "User created successfully", "OK");
                await Navigation.PopAsync(); //go back to the login page
            }
            else
            {
                await DisplayAlert("Error", "User with this email already exists", "OK");
            }
        }
    }
}