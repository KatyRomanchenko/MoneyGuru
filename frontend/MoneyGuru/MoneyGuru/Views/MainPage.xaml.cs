using SkiaSharp;
using SkiaSharp.Views.Desktop;
using Syncfusion.SfChart.XForms;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Xamarin.Forms.Internals.GIFBitmap;

namespace MoneyGuru
{
    public partial class MainPage
    {
        public TransactionViewModel TransactionViewModel { get; set; }

        public MainPage()
        {
            InitializeComponent();
            BarBackgroundColor = Color.FromHex("#E9C31E");
            TransactionViewModel = new TransactionViewModel();
            BindingContext = TransactionViewModel;

            var viewModel = (TransactionViewModel)BindingContext;
            ((DoughnutSeries)chart.Series[0]).ColorModel.Palette = ChartColorPalette.Custom;
            ((DoughnutSeries)chart.Series[0]).ColorModel.CustomBrushes = viewModel.CustomPalette;

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjQ3MzA3NEAzMjMxMmUzMDJlMzBZcmpqTkZQY2FlVCtjby84ajlwUEk2T25wTnhDd1FOUVNLeWVwWjVQY2pnPQ==;Mgo+DSMBaFt+QHJqVk1mQ1NDaV1CX2BZeFl0QmlYeE4BCV5EYF5SRHBdQl1lS3lScURmXX4=;Mgo+DSMBMAY9C3t2VFhiQlJPcEBAWnxLflF1VWVTfVl6dVJWESFaRnZdQV1lS35TcEdkWHhfd3VU;Mgo+DSMBPh8sVXJ1S0R+X1pCaV1FQmFJfFBmQ2lbeFRxc0U3HVdTRHRcQlthSX5VdEJiXn1edXU=;MjQ3MzA3OEAzMjMxMmUzMDJlMzBKUkduUTgzQWtsbUViWmdJdlpTMzN6Y0lVUjlLUjJDNHZXMnV1MmNNK01BPQ==;NRAiBiAaIQQuGjN/V0d+Xk9HfVldX2RWfFN0RnNfdVx3flRBcC0sT3RfQF5jT3xTdkNhXn5Zcn1VRA==;ORg4AjUWIQA/Gnt2VFhiQlJPcEBAWnxLflF1VWVTfVl6dVJWESFaRnZdQV1lS35TcEdkWHhdcXZU;MjQ3MzA4MUAzMjMxMmUzMDJlMzBRM0oxOGYrYThVMmFkZDNrcklidVhUY0xkTUR3NEdVclNJTi96RXAxeTBJPQ==;MjQ3MzA4MkAzMjMxMmUzMDJlMzBJZnhNOHpQTkNBWDZMZjJvVTRjNVYrTU5WcUEvMDlHWGd4M0FNMThpSElRPQ==;MjQ3MzA4M0AzMjMxMmUzMDJlMzBaTzhRSFBRZFFmeEpWVjV5MGJndVJVV0plN3FsM0thSnZiLzQ0NlpVTy80PQ==;MjQ3MzA4NEAzMjMxMmUzMDJlMzBjT1h0S0V6V2hsY055bkdlSVE3ZVhiTm10ejcwT3JuZ1dubXgvbUo3V3NVPQ==;MjQ3MzA4NUAzMjMxMmUzMDJlMzBZcmpqTkZQY2FlVCtjby84ajlwUEk2T25wTnhDd1FOUVNLeWVwWjVQY2pnPQ==");
        }

        private async void OnAddTransactionClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new AddTransactionPage());
        }

        private async void OnAddCategoryClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new AddCategoryPage());
        }

        private async void OnAddWalletClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new AddWalletPage());
        }

        private async void OnAddIncomeClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new AddIncomePage());
        }
        private async void OnCreateGoalClicked(object sender, EventArgs e)// To add GOALS logic
        {
            //Application.Current.MainPage = new CreateGoalPage();
        }

        private async void OnLogOutClicked(object sender, EventArgs e)
        {
            //LogOut from this account
            Application.Current.MainPage = new NavigationPage(new PrestartPage())
            {
                BarBackgroundColor = Color.FromHex("#7853FA"),
                BarTextColor = Color.Black
            };
        }

        private async void OnCurrencyCalculationClicked(object sender, EventArgs e)
        {
            //GOTO CurrencyCalc
            //await Navigation.PushAsync(new CurrencyCalculationPage());
            Application.Current.MainPage = new CurrencyCalculationPage();

        }
        private async void OnCalculationClicked(object sender, EventArgs e)
        {
            //Application.Current.MainPage = new CalculationPage();
        }
    }
}
