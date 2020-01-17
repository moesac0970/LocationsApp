using B4.PE3.VanLookManu.Domain.Models;
using B4.PE3.VanLookManu.Domain.Services;
using B4.PE3.VanLookManu.Domain.Services.Validator;
using FreshMvvm;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Color = System.Drawing.Color;

namespace B4.PE3.VanLookManu.ViewModels
{
    public class LocationViewModel : FreshBasePageModel
    {
        private readonly LocationService locationService;
        private readonly ILocationUserDataService locationUserDataService;
        private readonly IUserDataService userData;
        private LocationUser currentLocation;
        private bool isNew;
        private Validator validator;  

        public LocationViewModel(ILocationUserDataService _locationUserDataService, IUserDataService _userData)
        {
            locationService = new LocationService();
            this.locationUserDataService = _locationUserDataService;
            this.userData = _userData;
            this.validator = new Validator();
        }

        #region - Properties -
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        private string owner;
        public string Owner
        {
            get { return owner; }
            set
            {
                owner = value;
                RaisePropertyChanged(nameof(Owner));
            }
        }

        private string location;
        public string Location
        {
            get { return location; }
            set
            {
                location = value;
                RaisePropertyChanged(nameof(Location));
            }
        }

        private DateTime created;
        public DateTime Created
        {
            get { return created; }
            set
            {
                created = value;
                RaisePropertyChanged(nameof(Created));
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

        #region - Overrides -
        public async override void Init(object initData)
        {
            base.Init(initData);
            currentLocation = null;
            currentLocation = initData as LocationUser;
            if (currentLocation.Name == null) { isNew = true; }
            await LoadLocation();
        }

        public async override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);
            if (returnedData is Color)
            {
                Color _color = (Color)(returnedData as Color?);
                currentLocation.Color = _color;
                await LoadLocation();
            }
        }
        #endregion

        #region - Commands -
        public ICommand SaveLocationCommand => new Command(
           async () =>
           {
                if (validator.ValidatorLocation(new LocationUser { Name = name }).Count == 0) 
                { 

                    if (isNew)
                    {
                        currentLocation.Name = Name; currentLocation.Id = Guid.NewGuid();
                        await locationUserDataService.CreateLocation(currentLocation);
                    }
                    else
                    {
                        currentLocation.Name = Name; currentLocation.Color = Color.FromName(CurrentColor);
                        await locationUserDataService.UpdateLocation(currentLocation);
                    }

                   await CoreMethods.PushPageModel<MainViewModel>(true);
                   CoreMethods.RemoveFromNavigation();

               }
               else
               {
                   await CoreMethods.DisplayAlert("alert", "no name filled in", "ok");
               }

           }
        );
        public ICommand ColorSettingCommand => new Command(
           async () =>
           {
               if (isNew)
               {
                   currentLocation.Name = Name; currentLocation.Id = Guid.NewGuid();
               }
               else
               {
                   currentLocation.Name = Name;
                   await locationUserDataService.UpdateLocation(currentLocation);
               }
               await CoreMethods.PushPageModel<ColorSettingViewModel>(true);
           }
        );

        public ICommand OpenMaps => new Command(
           async () =>
           {
               //var map = new Map(MapSpan.FromCenterAndRadius(new Position(37, -122), Distance.FromMiles(10)));
               //var map = new Map();


               //var pin = new Pin()
               //{
               //    Position = new Position(37, -122),
               //    Label = "Some Pin!"
               //};
               //map.Pins.Add(pin);

               //var cp = new ContentPage
               //{
               //    Content = map,
               //};

               // xamarin forms map is not handy to work with

               await locationService.GeolocateVisualizedMark(currentLocation.Location.Longitude, currentLocation.Location.Latitude);
           });

        public ICommand OpenMapsRoute => new Command(
           async () =>
           {
               await locationService.GeolocateVisualizedRoute(currentLocation.Location, currentLocation.Name);
           });

        #endregion


        private async Task LoadLocation()
        {
            Name = currentLocation.Name;
            Location = $"latitude: {currentLocation.Location.Latitude}, longitude: {currentLocation.Location.Longitude}";
            Created = currentLocation.Created;
            var user = await userData.GetUserById(currentLocation.Owner);
            Owner = user.Name;
            CurrentColor = currentLocation.Color.Name;
        }


    }
}
