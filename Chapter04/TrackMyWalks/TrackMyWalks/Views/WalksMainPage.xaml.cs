//
//  WalksMainPage.xaml.cs
//  Displays Walk Information within a ListView control from an array 
//
//  Created by Steven F. Daniel on 14/05/2018
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using System;
using System.Collections.ObjectModel;
using TrackMyWalks.Models;
using Xamarin.Forms;

namespace TrackMyWalks.Views
{
    public partial class WalksMainPage : ContentPage
    {
        public WalksMainPage()
        {
            InitializeComponent();

            // Update the page title for our Main Page
            Title = "Track My Walks";
            this.InitialiseWalks();
        }

        public void InitialiseWalks()
        {
            // Create a collection that will raise an event, whenever an object is added or removed from our WalksListModel collection.
            var WalksListModel = new ObservableCollection<WalkDataModel> {
            // Populate our collection with some dummy data that will be used to populate our ListView
            new WalkDataModel
            {
                Id = 1,
                Title = "10 Mile Brook Trail, Margaret River",
                Description = "The 10 Mile Brook Trail starts in the Rotary Park near Old Kate, a preserved steam engine at the northern edge of Margaret River. ",
                Latitude = -33.9727604,
                Longitude = 115.0861599,
                Distance = 7.5,
                Difficulty = "Medium",
                ImageUrl = "http://trailswa.com.au/media/cache/media/images/trails/_mid/FullSizeRender1_600_480_c1.jpg"
            },
            new WalkDataModel
            {
                Id = 2,
                Title = "Ancient Empire Walk, Valley of the Giants",
                Description = "The Ancient Empire is a 450 metre walk trail that takes you around and through some of the giant tingle trees including the most popular of the gnarled veterans, known as Grandma Tingle.",
                Latitude = -34.9749188,
                Longitude = 117.3560796,
                Distance = 450,
                Difficulty = "Hard",
                ImageUrl = "http://trailswa.com.au/media/cache/media/images/trails/_mid/Ancient_Empire_534_480_c1.jpg"
            }};

            // Populate our ListView with entries from our WalksListModel
            WalkEntriesListView.ItemsSource = WalksListModel;
        }

        // Instance method to call the WalkEntryPage to add a Walk Entry
        public void AddWalk_Clicked(object sender, EventArgs e)
        {
            App.SelectedItem = null;
            Navigation.PushAsync(new WalkEntryPage());
        }

        // Instance method to call the WalkTrailInfoPage using the selected item
        public void myWalkEntries_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            // Get the selected item from our ListView
            App.SelectedItem = e.Item as WalkDataModel;
            Navigation.PushAsync(new WalkTrailInfoPage());
        }
    }
}