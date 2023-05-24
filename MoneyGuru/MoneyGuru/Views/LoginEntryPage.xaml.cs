using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System;
using System.Linq;
using Xamarin.Essentials;

namespace MoneyGuru
{
    public partial class LoginEntryPage
    {
        public LoginEntryPage()
        {
            InitializeComponent();
        }
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var user = App.Database.Table<User>().FirstOrDefault(x => x.Email == EmailEntry.Text);
            if (user != null)
            {
                if (user.Password == PasswordEntry.Text)  // Use hash comparison here
                {
                    // Log in user
                    await SecureStorage.SetAsync("userToken", user.Token); //var userToken = await SecureStorage.GetAsync("userToken"); to get it later
                    Application.Current.MainPage = new MainPage();
                }
                else
                {
                    await DisplayAlert("Error", "Invalid email or password", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Invalid email or password", "OK");
            }
        }
    }
}