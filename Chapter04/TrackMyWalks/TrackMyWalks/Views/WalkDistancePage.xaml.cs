//
//  WalkDistancePage.xaml.cs
//  Displays related trail information within a map using a pin placeholder
//
//  Created by Steven F. Daniel on 14/05/2018
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using System;
using TrackMyWalks.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TrackMyWalks.Views
{
    public partial class WalkDistancePage : ContentPage
    {
        public WalkDistancePage()
        {
            InitializeComponent();

            // Update the page title for our Distance Travelled Page
            Title = "Distance Travelled Information";

            // Create a pin placeholder within the map containing the walk information
            MyCustomTrailMap.Pins.Add(new Pin
            {
                Type = PinType.Place,
                Position = new Position(App.SelectedItem.Latitude, App.SelectedItem.Longitude),
                Label = App.SelectedItem.Title,
                Address = "Difficulty: " + App.SelectedItem.Difficulty + "  Total Distance: " + App.SelectedItem.Distance,
                Id = App.SelectedItem.Title
            });

            // Create a region around the map within a one-kilometer radius
            MyCustomTrailMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(App.SelectedItem.Latitude, App.SelectedItem.Longitude), Distance.FromKilometers(1.0)));
        }

        // Instance method that ends the current trail and returns back to the main screen
        public void EndThisTrailButton_Clicked(object sender, EventArgs e)
        {
            App.SelectedItem = null;
            Navigation.PopToRootAsync(true);
        }
    }
}