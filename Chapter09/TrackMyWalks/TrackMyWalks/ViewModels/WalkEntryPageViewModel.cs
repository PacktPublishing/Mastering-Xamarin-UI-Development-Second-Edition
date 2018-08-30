//
//  WalkEntryPageViewModel.cs
//  The ViewModel for our WalkEntryPage ContentPage
//
//  Created by Steven F. Daniel on 5/06/2018.
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using System;
using System.Threading.Tasks;
using TrackMyWalks.Models;
using TrackMyWalks.Services;

namespace TrackMyWalks.ViewModels
{
    public class WalkEntryPageViewModel : BaseViewModel
    {
        public WalkEntryPageViewModel(INavigationService navService) : base(navService)
        {
            // Update the title if we are creating a new Walk Entry
            if (App.SelectedItem == null)
            {
                PageTitle = "Adding Trail Details";
                App.SelectedItem = new WalkDataModel();

                // Set the default values when creating a new Trail
                Title = "New Trail Entry";
                Difficulty = "Easy";
                Distance = 1.0;
            }
            else
            {
                // Otherwise, we must be editing an existing entry
                PageTitle = "Editing Trail Details";
            }
        }

        // Checks to see if we have provided a Title and Description
        public bool ValidateFormDetailsAndSave()
        {
            if (App.SelectedItem != null && !string.IsNullOrEmpty(App.SelectedItem.Title) && !string.IsNullOrEmpty(App.SelectedItem.Description))
            {
                // Save the selected item to our database and/or model
            }
            else
            {
                return false;
            }
            return true;
        }

        // Get the current device GPS location Coordinates
        public async Task GetMyLocation()
        {
            // Get the current determined GPS position coordinates from the device
            var position = await new LocationService().GetCurrentPosition();
            if (position == null) return;

            // If we are Adding a new Walk Entry, update the Latitude and Longitude Coordinates
            if (App.SelectedItem.Latitude.Equals(0) && App.SelectedItem.Longitude.Equals(0))
            {
                Latitude = position.Latitude;
                Longitude = position.Longitude;
            }
        }

        // Update each EntryCell on the WalkEntryPage with values from our Model
        public string Title
        {
            get => App.SelectedItem.Title;
            set { App.SelectedItem.Title = value; OnPropertyChanged(); }
        }
        public string Description
        {
            get => App.SelectedItem.Description;
            set { App.SelectedItem.Description = value; OnPropertyChanged(); }
        }
        public double Latitude
        {
            get => App.SelectedItem.Latitude;
            set { App.SelectedItem.Latitude = value; OnPropertyChanged(); }
        }
        public double Longitude
        {
            get => App.SelectedItem.Longitude;
            set { App.SelectedItem.Longitude = value; OnPropertyChanged(); }
        }
        public double Distance
        {
            get => App.SelectedItem.Distance;
            set { App.SelectedItem.Distance = value; OnPropertyChanged(); }
        }
        public String Difficulty
        {
            get => App.SelectedItem.Difficulty;
            set { App.SelectedItem.Difficulty = value; OnPropertyChanged(); }
        }
        public String ImageUrl
        {
            get => App.SelectedItem.ImageUrl;
            set { App.SelectedItem.ImageUrl = value; OnPropertyChanged(); }
        }

        // Instance method to initialise the WalkEntryPageViewModel
        public override async Task Init()
        {
            await Task.Factory.StartNew(async () =>
            {
                // Call our GetMyLocation method to obtain our GPS Coordinates
                await GetMyLocation();
            });
        }
    }
}