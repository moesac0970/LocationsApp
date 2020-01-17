using B4.PE3.VanLookManu.Domain.Mock;
using B4.PE3.VanLookManu.Domain.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace B4.PE3.VanLookManu.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {

        public StartPage()
        {
            InitializeComponent();
            Title = "Welcome";
        
        }

    }
}