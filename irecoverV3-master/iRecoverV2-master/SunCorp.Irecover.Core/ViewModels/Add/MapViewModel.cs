using MvvmCross.Core.ViewModels;
using SunCorp.Irecover.Core.Interfaces2;
using SunCorp.Irecover.Core.Models2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunCorp.IRecover.ViewModels.Add
{
    public class MapViewModel : MvxViewModel
    {
        private GeoLocation myLocation;
        private readonly IGeoCoder geocoder;
        //private Action<GeoLocation, Forecast> weatherPinFound;
        private Action<GeoLocation, float> moveToLocation;
        public GeoLocation MyLocation
        {
            get { return myLocation; }
            set { myLocation = value; }
        }

        
        public MapViewModel(IGeoCoder geocoder)
        {
            this.geocoder = geocoder;
        }

        // Not an event handler, despite 'On'
        public void OnMyLocationChanged(GeoLocation geolocation)
        {
            MyLocation = geolocation;
        }

        //public void MapTapped(GeoLocation geoLocation)
        //{
        //    GetWeatherInfo(geoLocation);
        //}

        //private async Task GetWeatherInfo(GeoLocation geoLocation)
        //{
        //    var weatherService = new WeatherService();
        //    var city = await geocoder.GetCityFromLocation(geoLocation);
        //    var locationKey = await weatherService.GetLocations(city);
        //    var bestLocation = locationKey.FirstOrDefault();
        //    var forecast = await weatherService.GetForecast(bestLocation.Key);
        //    geoLocation.Locality = city;
        //    weatherPinFound(geoLocation, forecast);
        //}

        //public void OnMapSetUp(Action<GeoLocation, float> moveToLocation, Action<GeoLocation, Forecast> weatherPinFound)
        //{
        //    this.moveToLocation = moveToLocation;
        //    this.weatherPinFound = weatherPinFound;

        //}
    }
}
