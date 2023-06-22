using Microcharts;
using MoneyGuru.Services;
using Newtonsoft.Json;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using SkiaSharp.Views.Forms;
using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Xml;
using Xamarin.Forms;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static MoneyGuru.TransactionViewModel;
using static Xamarin.Forms.Internals.GIFBitmap;

namespace MoneyGuru
{
    public partial class MainPage
    {
        public TransactionViewModel TransactionViewModel { get; set; }
        //public List<ChartEntry> entries = new List<ChartEntry>();
        private ObservableCollection<AnalysisModel> _lineData;
        public ObservableCollection<AnalysisModel> LineData
        {
            get => _lineData;
            set
            {
                if (_lineData != value)
                {
                    _lineData = value;
                    OnPropertyChanged();
                }
            }
        }

        public MainPage()
        {
            InitializeComponent();
            BarBackgroundColor = Color.FromHex("#E9C31E");
            TransactionViewModel = new TransactionViewModel();
            BindingContext = TransactionViewModel;

            var viewModel = (TransactionViewModel)BindingContext;
            ((DoughnutSeries)chart.Series[0]).ColorModel.Palette = ChartColorPalette.Custom;
            ((DoughnutSeries)chart.Series[0]).ColorModel.CustomBrushes = viewModel.CustomPalette;

            LineChartView.Chart = new LineChart { 
                Entries = entries, 
                BackgroundColor = Color.FromHex("#7853FA").ToSKColor(), 
                LabelTextSize = 30, 
                LabelColor = Color.FromHex("#FFFFFF").ToSKColor(), 
                LabelOrientation = Orientation.Horizontal,
                ValueLabelOrientation= Orientation.Vertical,
            };

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
        private async void OnCreateGoalClicked(object sender, EventArgs e)
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
        private async void OnViewClicked(object sender, EventArgs e)
        {
            HttpClientFactory httpClientFactory = new HttpClientFactory();
            HttpClient client = httpClientFactory.CreateAuthenticatedClient();

            string selectedCategory = CategoryPicker.SelectedItem.ToString();

            var uri = new Uri(httpClientFactory.mainURL + $"api/transaction/byCategory/{selectedCategory}");
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var transactions = JsonConvert.DeserializeObject<List<Transaction>>(content);

                List<ChartEntry> entries = new List<ChartEntry>();

                foreach (var transaction in transactions)
                {
                    //thi should be entries for mainpage
                    entries.Add(new ChartEntry((float)transaction.Amount)
                    {
                        Label = transaction.Date.ToString("MM/dd/yyyy"),
                        ValueLabel = transaction.Amount.ToString(),
                        Color = SKColors.White
                    });
                }
            }
            else
            {
                await DisplayAlert("Error", "Could not fetch wallet", "OK");
            }
        }
        private async void FetchDataAsync()
        {
            HttpClientFactory httpClientFactory = new HttpClientFactory();
            HttpClient client = httpClientFactory.CreateAuthenticatedClient();
            //var selectedCategory = CategoryPicker.SelectedItem.ToString();

            var uri = new Uri("http://192.168.1.4:5000/api/transaction/byCategory/Sweets");
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var transactions = JsonConvert.DeserializeObject<List<Transaction>>(content);

                List<ChartEntry> entries = new List<ChartEntry>();

                foreach (var transaction in transactions)
                {
                    entries.Add(new ChartEntry((float)transaction.Amount)
                    {
                        Label = transaction.Date.ToString("MM/dd/yyyy"),
                        ValueLabel = transaction.Amount.ToString(),
                        Color = SKColors.White
                    });
                }

            }
        }
        private readonly ChartEntry[] entries = new[]
        {
            new ChartEntry(150)
            {
                Label = "12.3",
                ValueLabel = "150",
                Color = SKColors.White
            },
            new ChartEntry(500)
            {
                Label = "16.3",
                ValueLabel = "150",
                Color = SKColors.White
            },
            new ChartEntry(1500)
            {
                Label = "22.3",
                ValueLabel = "150",
                Color = SKColors.White
            },
        };

        public class Transaction
        {
            public decimal Amount { get; set; }
            public DateTime Date { get; set; }
        }
    }
}
