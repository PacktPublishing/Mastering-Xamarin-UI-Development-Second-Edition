//
//  WalkTrailInfoPageViewModel.cs
//  The ViewModel for our WalkTrailInfoPage ContentPage
//
//  Created by Steven F. Daniel on 5/06/2018.
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using System;
using System.Threading.Tasks;

namespace TrackMyWalks.ViewModels
{
    public class WalkTrailInfoPageViewModel : BaseViewModel
    {
        public WalkTrailInfoPageViewModel()
        {
        }

        // Update each control on the WalkTrailInfoPage with values from our Model
        public string Title => App.SelectedItem.Title;
        public string Description => App.SelectedItem.Description;
        public double Distance => App.SelectedItem.Distance;
        public String Difficulty => App.SelectedItem.Difficulty;
        public String ImageUrl => App.SelectedItem.ImageUrl;

        // Instance method to initialise the WalkTrailInfoPageViewModel
        public override async Task Init()
        {
            await Task.Factory.StartNew(() =>
            {
            });
        }
    }
}