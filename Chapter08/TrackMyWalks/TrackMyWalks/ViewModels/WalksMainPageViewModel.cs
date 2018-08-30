//
//  WalksMainPageViewModel.cs
//  The ViewModel for our WalksMainPage ContentPage
//
//  Created by Steven F. Daniel on 5/06/2018.
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TrackMyWalks.Models;
using TrackMyWalks.Services;

namespace TrackMyWalks.ViewModels
{
    public class WalksMainPageViewModel : BaseViewModel
    {
        // Create our WalksListModel Observable Collection
        public ObservableCollection<WalkDataModel> WalksListModel;

        public WalksMainPageViewModel(INavigationService navService) : base(navService)
        {
        }

        // Instance method to add and retrieve our  Walk Trail items
        public async Task GetWalkTrailItems()
        {
            // Check our IsProcessBusy property to see if we are already processing
            if (IsProcessBusy)
                return;

            // If we aren't processing, we need to set our IsProcessBusy property to true
            IsProcessBusy = true;

            // Specify our List Collection to store the items being read
            WalksListModel = new ObservableCollection<WalkDataModel> {

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

            // Add a temporary timer, so that we can see our progress indicator working
            await Task.Delay(3000);

            // Set our IsProcessBusy property value back to false when finished
            IsProcessBusy = false;
        }

        // Instance method to initialise the WalksMainPageViewModel
        public override async Task Init()
        {
            await Task.Factory.StartNew(async () =>
            {
                // Call our GetWalkTrailItems method to populate our collection
                await GetWalkTrailItems();
            });
        }
    }
}