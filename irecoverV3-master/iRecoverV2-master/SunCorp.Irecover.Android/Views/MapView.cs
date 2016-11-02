using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvvmCross.Core.ViewModels;
using SunCorp.Irecover.Core.Interfaces2;
using SunCorp.Irecover.Core.Models2;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Views;
using SunCorp.Irecover.Core.ViewModels;
using Android.Gms.Maps;

namespace SunCorp.Irecover.Android.Views
{
    [Activity(Label = "MapView")]
    public class MapView : MvxActivity
    {
        MapViewModel vm;
        GoogleMap map; 
        public void OnMapReady(GoogleMap googleMap)
        {
            vm.OnMapSetup(MoveToLocation, AddWeatherPin);
            map = googleMap;
            map.MyLocationEnabled = true;
            map.MyLocationChange += Map_MyLocationChange;
            map.MapLongClick += Map_MapClick;
            map.MapLongClick += Map_MapLongClick;
        }

        private void Map_MyLocationChange(object sender, GoogleMap.MyLocationChangeEventArgs e)
        {
            map.MyLocationChange -= Map_MyLocationChange;
            var location = new GeoLocation(e.Location.Latitude, e.Location.Longitude, e.Location.Altitude);
            MoveToLocation(location);
            vm.OnMyLocationChanged(location);
        }


        private void Map_MapClick(object sender, GoogleMap.MapLongClickEventArgs e)
        {
            vm.MapTapped(new GeoLocation(e.Point.Latitude, e.Point.Longitude));
        }


        private void AddWeatherPin(GeoLocation location, Forecast forecast)
        {
            var markerOptions = new MarkerOptions();
            markerOptions.SetPosition(new LatLng(location.Latitude, location.Longitude));
            var min = forecast.DailyForecasts.FirstOrDefault().Temperature.Minimum;
            var max = forecast.DailyForecasts.FirstOrDefault().Temperature.Maximum;
            markerOptions.SetSnippet(string.Format("Min {0}{1}, Max {2}{3}", min.Value, min.Unit, max.Value, max.Unit));
            markerOptions.SetTitle(location.Locality);
            map.AddMarker(markerOptions);
        }

        private void MoveToLocation(GeoLocation geoLocation, float zoom = 18)
        {
            CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
            builder.Target(new LatLng(geoLocation.Latitude, geoLocation.Longitude));
            builder.Zoom(zoom);
            var cameraPosition = builder.Build();
            var cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);
            map.MoveCamera(cameraUpdate);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MapLayout);
            vm = ViewModel as MapViewModel;
            var mapFragment = FragmentManager.FindFragmentById(Resource.Id.map) as MapFragment;
            mapFragment.GetMapAsync(this);

        }
    }
}