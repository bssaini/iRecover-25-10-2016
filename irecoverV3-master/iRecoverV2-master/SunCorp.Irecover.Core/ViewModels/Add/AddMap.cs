using MvvmCross.Core.ViewModels;
using SunCorp.Irecover.Core.Models;
// using SunCorp.Irecover.Core.Services;
using SunCorp.IRecover.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunCorp.IRecover.ViewModels.Add
{
    public class AddMap : MvxViewModel
    {
        private GeoLocation myLocation;
        private IGeoCoder geocoder;
        private Action<GeoLocation, float> moveToLocation;
        private Action<GeoLocation, Forecast> weatherPinFound;
        public GeoLocation MyLocation
        {
            get { return myLocation; }
            set { myLocation = value; }
        }

        public WeatherMapViewModel(IGeoCoder geocoder)
        {
            this.geocoder = geocoder;
        }
        public void OnMyLocationChanged(GeoLocation location)
        {
            MyLocation = location;
        }
        public void MapTapped(GeoLocation location)
        {
            GetWeatherInfo(location);
        }

        private async Task GetWeatherInfo(GeoLocation location)
        {
            var weatherService = new WeatherService();
            var city = await geocoder.GetCityFromLocation(location);
            var locationKey = await weatherService.GetLocations(city);
            var bestLocation = locationKey.FirstOrDefault();
            var forecast = await weatherService.GetForecast(bestLocation.Key);
            location.Locality = city;
            weatherPinFound(location, forecast);
        }

        public void OnMapSetup(Action<GeoLocation, float> MoveToLocation, Action<GeoLocation, Forecast> WeatherPinFound)
        {
            moveToLocation = MoveToLocation;
            weatherPinFound = WeatherPinFound;
        }
    }
}
