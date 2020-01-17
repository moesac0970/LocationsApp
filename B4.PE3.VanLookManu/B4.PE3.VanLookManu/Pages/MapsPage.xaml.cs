using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace B4.PE3.VanLookManu.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapsPage : ContentPage
    {
        public MapsPage()
        {
            InitializeComponent();


            var map = new Map();


            var pin = new Pin()
            {
                Position = new Position(37, -122),
                Label = "Some Pin!"
            };
            map.Pins.Add(pin);

            var cp = new ContentPage
            {
                Content = map,
            };
        }
    }
}