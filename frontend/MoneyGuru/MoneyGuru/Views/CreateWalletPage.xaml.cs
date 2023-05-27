using MoneyGuru.ViewModels;
using Xamarin.Forms;

namespace MoneyGuru.Views
{
    public partial class CreateWalletPage : ContentPage
    {
        public CreateWalletPage()
        {
            InitializeComponent();
            BindingContext = new CreateWalletViewModel();
        }
    }
}
