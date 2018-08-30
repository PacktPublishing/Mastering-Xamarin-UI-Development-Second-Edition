//
//  WalkTrailInfoPage.xaml.cs
//  Displays related trail information chosen from the WalksMainPage
//
//  Created by Steven F. Daniel on 14/05/2018
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using System;
using Xamarin.Forms;
using TrackMyWalks.Models;

namespace TrackMyWalks.Views
{
    public partial class WalkTrailInfoPage : ContentPage
    {
        public WalkTrailInfoPage()
        {
            InitializeComponent();

            // Update the page title for our Walk Information Page
            Title = "Trail Walk Information";

            // Set the Binding Context for our ContentPage
            this.BindingContext = App.SelectedItem;
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