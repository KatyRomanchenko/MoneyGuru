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
            MainPage = new PrestartPage();
        }


        protected override void OnStart() 
        {
        }
        //protected override void OnSleep() { }
        //protected override void OnResume() { }
    }
}
