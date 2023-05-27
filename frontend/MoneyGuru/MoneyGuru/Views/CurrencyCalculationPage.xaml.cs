using MoneyGuru.ViewModels;
using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MoneyGuru
{
    public partial class CurrencyCalculationPage
    {
        double currentNumber, finalNumber;
        string mathOperator;
        public CurrencyCalculationPage()
        {
            InitializeComponent();
        }
        async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
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
