//
//  WalkDistancePagePageViewModel.cs
//  The ViewModel for our WalkDistancePage ContentPage
//
//  Created by Steven F. Daniel on 5/06/2018
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using System;
using System.Threading.Tasks;

namespace TrackMyWalks.ViewModels
{
    public class WalkDistancePageViewModel : BaseViewModel
    {
        public WalkDistancePageViewModel()
        {
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