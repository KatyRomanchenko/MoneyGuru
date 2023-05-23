using Xamarin.Forms;

namespace MoneyGuru
{
    public partial class App : Application
    {
        //private static DataService _dataService;

        //public static DataService DataService => _dataService ?? (_dataService = new DataService());
        public App()
        {
            //InitializeComponent();
            //MainPage.Navigation.PopToRootAsync(MainPage());
            MainPage = new PrestartPage();
        }

        protected override void OnStart()
        {
            //подключиться к БД
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
