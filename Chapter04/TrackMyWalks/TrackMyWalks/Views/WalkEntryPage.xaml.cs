//
//  WalkEntryPage.xaml.cs
//  Data Entry screen that allows new walk information to be added 
//
//  Created by Steven F. Daniel on 14/05/2018
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using System;
using Xamarin.Forms;

namespace TrackMyWalks.Views
{
    public partial class WalkEntryPage : ContentPage
    {
        public WalkEntryPage()
        {
            InitializeComponent();

            // Update the page title for our Walks Entry Page
            Title = "New Walk Entry Page";
        }

        // Instance method that saves the new walk entry
        public void SaveWalkItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PopToRootAsync(true);
        }
    }
}