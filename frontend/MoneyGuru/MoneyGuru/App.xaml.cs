using Xamarin.Forms;

namespace MoneyGuru
{
    public partial class App : Application
    {
        public App()
        {
            MainPage = new NavigationPage(new MainPage()) 
            {
                BarBackgroundColor = Color.FromHex("#7853FA")
            };
        }
    }
}