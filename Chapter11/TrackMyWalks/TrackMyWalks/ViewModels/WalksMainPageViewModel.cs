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

            // Populate our WalkListModel List Collection with items from our Microsoft Azure Web Service
            WalksListModel = new ObservableCollection<WalkDataModel>(await AzureDatabase.GetWalkEntries());

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