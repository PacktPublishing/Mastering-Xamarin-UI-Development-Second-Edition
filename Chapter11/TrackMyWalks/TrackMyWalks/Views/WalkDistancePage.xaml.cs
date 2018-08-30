//
//  WalkDistancePage.xaml.cs
//  Displays related trail information within a map using a pin placeholder
//
//  Created by Steven F. Daniel on 14/05/2018
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using System;
using Plugin.Geolocator.Abstractions;
using TrackMyWalks.Services;
using TrackMyWalks.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Threading.Tasks;
using TrackMyWalks.Views.MapOverlay;

namespace TrackMyWalks.Views
{
    public partial class WalkDistancePage : ContentPage
    {
        // Return the Binding Context for the ViewModel
        WalkDistancePageViewModel _viewModel => BindingContext as WalkDistancePageViewModel;

        // Create a variable that will store our original saved Position
        Task<Plugin.Geolocator.Abstractions.Position> origPosition;

        public WalkDistancePage()
        {
            InitializeComponent();

            // Update the Title and Initialise our BindingContext for the Page
            Title = "Distance Travelled Information";
            this.BindingContext = new WalkDistancePageViewModel(DependencyService.Get<INavigationService>());

            // Get the current GPS location coordinates and listen for updates
            origPosition = _viewModel.GetCurrentLocation();
            _viewModel.CoordsChanged += Location_PositionChanged;
            _viewModel.OnStartUpdate();

            // Instantiate our Custom Map Overlay
            customMap = new CustomMapOverlay
            {
                MapType = MapType.Street
            };

            // Clear all previously created Pins on our CustomMap
            customMap.Pins.Clear();

            // Create the Pin placeholder that will represent our current location
            CreatePinPlaceholder(PinType.Place,
                        origPosition.Result.Latitude,
                        origPosition.Result.Longitude,
            "",
            "My Location", 1);

            // Create the Pin placeholder that will represent our ending location
            CreatePinPlaceholder(PinType.Place,
                                 _viewModel.Latitude,
                                 _viewModel.Longitude,
                                 _viewModel.Title,
                                 "Difficulty: " + _viewModel.Difficulty + " Total Distance: " + _viewModel.Distance,
                                 2);

            // Add the Starting and Ending Latitude and Longitude Coordinates
            customMap.RouteCoordinates.Add(new Xamarin.Forms.Maps.Position(origPosition.Result.Latitude, origPosition.Result.Longitude));
            customMap.RouteCoordinates.Add(new Xamarin.Forms.Maps.Position(_viewModel.Latitude, _viewModel.Longitude));

            // Create and Initialise a map region within a one-kilometre radius
            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(origPosition.Result.Latitude, origPosition.Result.Longitude), Distance.FromKilometers(1)));

            // Display our Custom Map for the detected device Platform
            Content = customMap;
        }

        // Instance method to handle updating the UI whenever the location changes
        void Location_PositionChanged(object sender, PositionEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                // Calculate the total distance travelled from the origPosition to the Current GPS Coordinate
                var distanceTravelled = origPosition.Result.CalculateDistance(e.Position, GeolocatorUtils.DistanceUnits.Kilometers);

                // Create a new Pin Placeholder, showing the current GPS Coordinate and the distance travelled
                CreatePinPlaceholder(PinType.SavedPin,
                                     e.Position.Latitude,
                                     e.Position.Longitude,
                                     String.Format("Travelled: {0:0.00} KM", distanceTravelled),
                                     "",
                                     3);
            });
        }

        // Instance method to create a pin placeholder to the custom map
        public void CreatePinPlaceholder(PinType pinType, double latitude, double longitude, String label, String address, int Id)
        {
            customMap.Pins.Add(new Pin
            {
                Type = pinType,
                Position = new Xamarin.Forms.Maps.Position(latitude, longitude),
                Label = label,
                Address = address,
                Id = Id
            });

            // Show the users current location on the map
            customMap.IsShowingUser = true;
        }

        // Instance method that ends the current trail and returns back to the main screen
        public async void EndTrailButton_Clicked(object sender, EventArgs e)
        {
            // Stop listening for location updates prior to navigating
            App.SelectedItem = null;
            _viewModel.OnStopUpdate();
            await _viewModel.Navigation.BackToMainPage();
        }
    }
}