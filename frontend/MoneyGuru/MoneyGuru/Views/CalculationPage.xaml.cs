using MoneyGuru.ViewModels;
using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MoneyGuru
{
    public partial class CalculationPage
    {
        double currentNumber, finalNumber;
        string mathOperator;
        public CalculationPage()
        {
            InitializeComponent();
        }
        async void OnBackButtonClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.FromHex("#7853FA"),
                BarTextColor = Color.Black
            };
        }

        void OnSelectNumber(object sender, EventArgs e)
        {
            var button = (Button)sender;
            display.Text += button.Text;
            currentNumber = Double.Parse(display.Text);
        }

        void OnSelectOperator(object sender, EventArgs e)
        {
            var button = (Button)sender;
            mathOperator = button.Text;
            finalNumber = currentNumber;
            display.Text = "";
        }

        void OnCalculate(object sender, EventArgs e)
        {
            switch (mathOperator)
            {
                case "+":
                    display.Text = (finalNumber + currentNumber).ToString();
                    break;
                case "-":
                    display.Text = (finalNumber - currentNumber).ToString();
                    break;
                case "*":
                    display.Text = (finalNumber * currentNumber).ToString();
                    break;
                case "/":
                    display.Text = (finalNumber / currentNumber).ToString();
                    break;
            }
        }

        void OnClear(object sender, EventArgs e)
        {
            display.Text = "";
            currentNumber = 0;
            finalNumber = 0;
        }
    }
}
