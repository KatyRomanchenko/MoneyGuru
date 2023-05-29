using SQLite;
using Syncfusion.SfDataGrid.XForms.DataPager;
using System;
using System.IO;
using Xamarin.Forms;

namespace MoneyGuru
{
    public partial class App : Application
    {
        public App()
        {
            MainPage = new NavigationPage(new PrestartPage()) 
            {
                BarBackgroundColor = Color.FromHex("#7853FA")
            };
        }
    }
}