//
//  WalkDistancePage.xaml.cs
//  Displays related trail information within a map using a pin placeholder
//
//  Created by Steven F. Daniel on 14/05/2018
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using System;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator.Abstractions;
using TrackMyWalks.Services;
using TrackMyWalks.ViewModels;
using TrackMyWalks.Views.MapOverlay;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TrackMyWalks.Views
{
    public partial class WalkDistancePage : ContentPage
    {
        // Return the Binding Context for the ViewModel
        WalkDistancePageViewModel _viewModel => BindingContext as WalkDistancePageViewModel;

        // Create a variable that will store our original saved Position
        Task<Plugin.Geolocator.Abstractions.Position> origPosition;

        // Create a TwitterObject variable that will contain an instance to our TwitterWebService class
        TwitterWebService TwitterObject;

        public WalkDistancePage()
        {
            InitializeComponent();

            // Create an instance to our TwitterWebService class
            //TwitterObject = new TwitterWebService(TwitterAuthDetails.ConsumerKey,
            //TwitterAuthDetails.ConsumerSecret,
            //TwitterAuthDetails.AuthToken, TwitterAuthDetails.AuthTokenSecret);

            TwitterObject = new TwitterWebService();

            // Update the Title and Initialise our BindingContext for the Page
            Title = "Distance Travelled Information";
            this.BindingContext = new WalkDistancePageViewModel(DependencyService.Get<INavigationService>());

            // Get the current GPS location coordinates and listen for updates
            origPosition = _viewModel.GetCurrentLocation();
            _viewModel.CoordsChanged += Location_PositionChanged;
            _viewModel.OnStartUpdate();

            // Instantiate our Custom Map Overlay
            customMap = new CustomMapOverlay
            {
                MapType = MapType.Street
            };

            // Clear all previously created Pins on our CustomMap
            customMap.Pins.Clear();

            // Create the Pin placeholder that will represent our current location
            CreatePinPlaceholder(PinType.Place,
                        origPosition.Result.Latitude,
                        origPosition.Result.Longitude,
            "",
            "My Location", 1);

            // Create the Pin placeholder that will represent our ending location
            CreatePinPlaceholder(PinType.Place,
                                 _viewModel.Latitude,
                                 _viewModel.Longitude,
                                 _viewModel.Title,
                                 "Difficulty: " + _viewModel.Difficulty + " Total Distance: " + _viewModel.Distance,
                                 2);

            // Add the Starting and Ending Latitude and Longitude Coordinates
            customMap.RouteCoordinates.Add(new Xamarin.Forms.Maps.Position(origPosition.Result.Latitude, origPosition.Result.Longitude));
            customMap.RouteCoordinates.Add(new Xamarin.Forms.Maps.Position(_viewModel.Latitude, _viewModel.Longitude));

            // Create and Initialise a map region within a one-kilometre radius
            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(origPosition.Result.Latitude, origPosition.Result.Longitude), Distance.FromKilometers(1)));

            // Display our Custom Map for the detected device Platform
            Content = customMap;
        }

        // Instance method to handle updating the UI whenever the location changes
        void Location_PositionChanged(object sender, PositionEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                // Calculate the total distance travelled from the origPosition to the Current GPS Coordinate
                var distanceTravelled = origPosition.Result.CalculateDistance(e.Position, GeolocatorUtils.DistanceUnits.Kilometers);

                // Create a new Pin Placeholder, showing the current GPS Coordinate and the distance travelled
                CreatePinPlaceholder(PinType.SavedPin,
                                     e.Position.Latitude,
                                     e.Position.Longitude,
                                     String.Format("Travelled: {0:0.00} KM", distanceTravelled),
                                     "",
                                     3);
            });
        }

        // Instance method to create a pin placeholder to the custom map
        public void CreatePinPlaceholder(PinType pinType, double latitude, double longitude, String label, String address, int Id)
        {
            customMap.Pins.Add(new Pin
            {
                Type = pinType,
                Position = new Xamarin.Forms.Maps.Position(latitude, longitude),
                Label = label,
                Address = address,
                Id = Id
            });

            // Show the users current location on the map
            customMap.IsShowingUser = true;
        }

        // Instance method that presents the user with additional options
        public async void OptionsButton_Clicked(object sender, EventArgs e)
        {
            // Display our Action Sheet with a list of choices for the user to choose
            var action = await DisplayActionSheet("What would you like to do?",
                                                  "Cancel", null, "Show Twitter Profile",
                                                  "Post Twitter Message", "End Current Trail");
            switch (action)
            {
                case "Show Twitter Profile":
                    ShowTwitterProfile();
                    break;
                case "Post Twitter Message":
                    PostTwitterMessage();
                    break;
                case "End Current Trail":
                    EndTrailButton_Clicked(sender, e);
                    break;
            }
        }

        // Instance method to get our Twitter Profile Details
        public async void ShowTwitterProfile()
        {
            // Call our Instance method to get the user's Twitter Profile Details
            var ProfileInfo = await TwitterObject.GetTwitterProfile(TwitterAuthDetails.AuthAccount);

            // Construct our message to display within an alert dialog
            var profileDetails = new StringBuilder();
            profileDetails.AppendFormat("\nId: {0}", ProfileInfo.GetValue("id"));
            profileDetails.AppendFormat("\nName: {0}", ProfileInfo.GetValue("name"));
            profileDetails.AppendFormat("\nScreen Name: {0}", ProfileInfo.GetValue("screen_name"));
            profileDetails.AppendFormat("\nLocation: {0}", ProfileInfo.GetValue("location"));
            profileDetails.AppendFormat("\nDescription: {0}", ProfileInfo.GetValue("description"));
            profileDetails.AppendFormat("\nFriends: {0}", ProfileInfo.GetValue("friends_count"));
            profileDetails.AppendFormat("\nFollowers: {0}", ProfileInfo.GetValue("followers_count"));
            profileDetails.AppendFormat("\nFavourites: {0}", ProfileInfo.GetValue("favourites_count"));
            profileDetails.AppendFormat("\nurl: {0}", ProfileInfo.GetValue("url"));

            // Display an alert dialog with the user's profile details
            await DisplayAlert("Twitter Profile Details", profileDetails.ToString(), "OK");
        }

        // Instance method to allow the user to post a message to their Twitter Feed
        public async void PostTwitterMessage()
        {
            // Construct our message to post to the users Twitter Feed
            var sbMessage = new StringBuilder();
            sbMessage.AppendLine("Track My Walks - Trail Details");
            sbMessage.AppendFormat("\nTitle: {0}", App.SelectedItem.Title);
            sbMessage.AppendFormat("\nDistance: {0}", App.SelectedItem.Distance);
            sbMessage.AppendFormat("\nDifficulty: {0}", App.SelectedItem.Difficulty);
            sbMessage.AppendFormat("\nImageURL: {0}", App.SelectedItem.ImageUrl);

            // Call our Instance method to Tweet the message to the users twitter page
            // We need to truncate our string so that it is within the Twitter allowable message constraints
            var tweet = sbMessage.ToString().Substring(0, 128);
            var response = TwitterObject.TweetMessage(tweet, TwitterAuthDetails.AuthAccount);

            // Display an alert dialog to let the user know their message has been posted.
            await DisplayAlert("Posted to Twitter", "Trail Information has been posted.", "OK");
        }

        // Instance method to terminate the current Trail
        public async void EndTrailButton_Clicked(object sender, EventArgs e)
        {
            // Initialise our Selected Item property
            App.SelectedItem = null;

            // Stop listening for location updates prior to navigating
            _viewModel.OnStopUpdate();

            // Navigate back to the Track My Walks Listing Page
            await _viewModel.Navigation.BackToMainPage();
        }
    }
}