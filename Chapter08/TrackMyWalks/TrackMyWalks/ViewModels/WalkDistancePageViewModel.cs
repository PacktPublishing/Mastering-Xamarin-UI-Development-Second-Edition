//
//  WalkDistancePagePageViewModel.cs
//  The ViewModel for our WalkDistancePage ContentPage
//
//  Created by Steven F. Daniel on 5/06/2018
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using System;
using System.Threading.Tasks;
using TrackMyWalks.Services;
using Plugin.Geolocator.Abstractions;

namespace TrackMyWalks.ViewModels
{
    public class WalkDistancePageViewModel : BaseViewModel
    {
        // Initialise our location service variable that points to our LocationService class
        LocationService location;
        public event EventHandler<PositionEventArgs> CoordsChanged;

        public WalkDistancePageViewModel(INavigationService navService) : base(navService)
        {
        }

        // Instance method to get the current GPS location Coordinates from device
        public async Task<Position> GetCurrentLocation()
        {
            // Initialise our location service variable that points to our LocationService class
            location = new LocationService();
            location.LocationChanged += (sender, e) =>
            {
                // Raise our PositionChanged EventHandler, using the Coordinates
                CoordsChanged.Invoke(sender, e);
            };

            // Get the current device GPS location coordinates
            var position = await location.GetCurrentPosition();
            return position;
        }

        // Instance method to begin listening for changes in GPS coordinates
        public async void OnStartUpdate()
        {
            await location.StartListening();
        }

        // Instance method to stop listening for changes in location
        public void OnStopUpdate()
        {
            location.StopListening();
        }

        // Update each control on the WalkDistancePage with values from our Model
        public string Title => App.SelectedItem.Title;
        public string Description => App.SelectedItem.Description;
        public double Latitude => App.SelectedItem.Latitude;
        public double Longitude => App.SelectedItem.Longitude;
        public double Distance => App.SelectedItem.Distance;
        public String Difficulty => App.SelectedItem.Difficulty;
        public String ImageUrl => App.SelectedItem.ImageUrl;

        // Instance method to initialise the WalkDistancePageViewModel
        public override async Task Init()
        {
            await Task.Factory.StartNew(() =>
            {
            });
        }
    }
}