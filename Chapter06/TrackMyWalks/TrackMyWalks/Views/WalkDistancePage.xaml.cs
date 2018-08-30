//
//  WalkDistancePage.xaml.cs
//  Displays related trail information within a map using a pin placeholder
//
//  Created by Steven F. Daniel on 14/05/2018
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using System;
using TrackMyWalks.Services;
using TrackMyWalks.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TrackMyWalks.Views
{
    public partial class WalkDistancePage : ContentPage
    {
        // Return the Binding Context for the ViewModel
        WalkDistancePageViewModel _viewModel => BindingContext as WalkDistancePageViewModel;

        public WalkDistancePage()
        {
            InitializeComponent();

            // Update the Title and Initialise our BindingContext for the Page
            Title = "Distance Travelled Information";
            this.BindingContext = new WalkDistancePageViewModel(DependencyService.Get<INavigationService>());

            // Create a pin placeholder within the map containing the walk information
            customMap.Pins.Add(new Pin
            {
                Type = PinType.Place,
                Position = new Position(_viewModel.Latitude, _viewModel.Longitude),
                Label = _viewModel.Title,
                Address = "Difficulty: " + _viewModel.Difficulty + "  Total Distance: " + _viewModel.Distance,
                Id = _viewModel.Title
            });

            // Create a region around the map within a one-kilometer radius
            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(_viewModel.Latitude, _viewModel.Longitude), Distance.FromKilometers(1.0)));
        }

        // Instance method that ends the current trail and returns back to the main screen
        public async void EndThisTrailButton_Clicked(object sender, EventArgs e)
        {
            App.SelectedItem = null;
            await _viewModel.Navigation.BackToMainPage();
        }
    }
}