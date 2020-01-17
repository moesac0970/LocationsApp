using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace B4.PE3.VanLookManu.Domain.Services
{
    public class LocationService
    {

        public async Task<string> LocationploxAsync()
        {
            string respons = "";
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.High);
                var location = await Geolocation.GetLocationAsync(request);

                if (location.IsFromMockProvider)
                {
                    respons = "location is mock";
                }
                else
                {
                    respons = "location received";
                }
            }
            catch (FeatureNotSupportedException)
            {
                respons = "location service isn't supported on your device, check ur settings";
            }
            catch (FeatureNotEnabledException)
            {
                respons = "location service isn't enabled on your device";
            }
            catch (PermissionException)
            {
                respons = "location service isn't permitted, check your aplication permission settings";
            }
            catch
            {
                respons = "something went teribly wron";
            }
            return respons;
        }

        public async Task GeolocateVisualizedMark(double lng, double lat)
        {
            var placemark = new Placemark
            {
                Location = new Location(lat, lng)
            };

            await Map.OpenAsync(placemark);
        }

        public async Task GeolocateVisualizedRoute(Location loc1, string name)
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            var location = await Geolocation.GetLocationAsync(request);
            var options = new MapLaunchOptions { Name = name, NavigationMode = NavigationMode.Walking };

            await Map.OpenAsync(location, options);
        }

        public async Task<Location> Locate()
        {
            try
            {


                var request = new GeolocationRequest(GeolocationAccuracy.Best);
                var location = await Geolocation.GetLocationAsync(request);
                return location;
            }
            catch (Exception ex)
            {
                return new Location();
            }
        }
    }

}
