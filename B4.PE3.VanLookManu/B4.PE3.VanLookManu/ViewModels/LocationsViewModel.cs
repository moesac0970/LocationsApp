using B4.PE3.VanLookManu.Domain.Models;
using B4.PE3.VanLookManu.Domain.Services;
using FreshMvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace B4.PE3.VanLookManu.ViewModels
{
    public class LocationsViewModel : FreshBasePageModel
    {
        private User currentUser;
        private readonly ILocationUserDataService data;
        private readonly LocationService locationService;
        public LocationsViewModel(ILocationUserDataService _data)
        {
            this.data = _data;
            locationService = new LocationService();

        }

        #region - Overrides -
        public async override void Init(object initData)
        {
            base.Init(initData);

            currentUser = initData as User;
            await RefreshLocations();
        }

        protected async override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await RefreshLocations();
        }

        #endregion

        #region - Properties -
        private ObservableCollection<LocationUser> locations;
        public ObservableCollection<LocationUser> Locations
        {
            get { return locations; }
            set
            {
                locations = value;
                RaisePropertyChanged(nameof(LocationUser));
            }
        }
        private string color;
        public string CurrentColor
        {
            get { return color; }
            set
            {
                color = value;
                RaisePropertyChanged(nameof(CurrentColor));
            }
        }
        #endregion

        #region - Commands -
        public ICommand OpenUserLocationCommand => new Command<ItemTappedEventArgs>(
          async (ItemTappedEventArgs args) =>
          {
              await CoreMethods.PushPageModel<LocationViewModel>((args.Item as LocationUser), false, true);
          });


        public ICommand NewLocationCommand => new Command<LocationUser>(
            async (LocationUser item) =>
            {

                try
                {
                    var location = await locationService.Locate();

                    if (location != null)
                    {
                        item = new LocationUser
                        {
                            Owner = currentUser.Id,
                            Location = location,
                            Created = DateTime.Now,
                            Color = System.Drawing.Color.Red // standard color 
                        };
                        await CoreMethods.PushPageModel<LocationViewModel>(item, false, true);
                    }
                    else
                    {
                        string alert = await locationService.LocationploxAsync();
                        await DisplayAlertInfo(alert);
                        // alternatief ==>
                        //await CoreMethods.DisplayAlert("error", alert, "cancel");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlertInfo(ex.ToString());
                    // alternatief ==>
                    //await CoreMethods.DisplayAlert("error", ex, "cancel");
                }
            }
        );
        #endregion

        private async Task RefreshLocations()
        {
            var locations = await data.GetLocationsAsync(currentUser.Id);
            Locations = null;
            Locations = new ObservableCollection<LocationUser>(locations.OrderBy(loc => loc.Name));
        }

        private async Task DisplayAlertInfo(string alert)
        {
            await App.Current.MainPage.DisplayAlert("ow ow rusteug", $"{alert}", "ok");
        }


    }
}
