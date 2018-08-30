//
//  WalkTrailInfoPage.xaml.cs
//  Displays related trail information chosen from the WalksMainPage
//
//  Created by Steven F. Daniel on 14/05/2018
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using System;
using Xamarin.Forms;
using TrackMyWalks.ViewModels;

namespace TrackMyWalks.Views
{
    public partial class WalkTrailInfoPage : ContentPage
    {
        // Return the Binding Context for the ViewModel
        WalkTrailInfoPageViewModel _viewModel => BindingContext as WalkTrailInfoPageViewModel;

        public WalkTrailInfoPage()
        {
            InitializeComponent();

            // Update the Title and Initialise our BindingContext for the Page
            this.Title = "Trail Walk Information";
            this.BindingContext = new WalkTrailInfoPageViewModel();
        }

        // Instance method that proceeds to begin a new walk trail
        public void BeginTrailWalk_Clicked(object sender, EventArgs e)
        {
            if (App.SelectedItem == null)
                return;

            Navigation.PushAsync(new WalkDistancePage());
            Navigation.RemovePage(this);
        }
    }
}