using System;
using System.Linq;
using System.Threading.Tasks;
using SunCorp.Irecover.Core.Interfaces2;
using SunCorp.Irecover.Core.Models2;
using Android.App;

namespace SunCorp.Irecover.Core.Services2
{
    public class GeoCoderService : IGeoCoder
    {
        public async Task<string> GetCityFromLocation(GeoLocation location)
        {
            var geocoder = new Android.Locations.Geocoder(Application.Context);
            var foundLocation = await geocoder.GetFromLocationAsync(location.Latitude, location.Longitude, 1);
            return foundLocation.FirstOrDefault().Locality;
        }


        public async Task<GeoLocation> GetLocationFromAddress(string address)
        {
            using (var geocoder = new Android.Locations.Geocoder(Application.Context))
            {
                var foundAddress = await geocoder.GetFromLocationNameAsync(address, 1);
                var geoLocation = foundAddress.FirstOrDefault();
                return new GeoLocation(geoLocation.Latitude, geoLocation.Longitude);
            }
        }
    }
}
