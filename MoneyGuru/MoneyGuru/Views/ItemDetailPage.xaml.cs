using MoneyGuru.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MoneyGuru.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}