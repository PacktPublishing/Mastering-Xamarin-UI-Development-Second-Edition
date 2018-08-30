//
//  LocationService.cs
//  Location Service Class that will be used retrieve GPS Coordinates
//
//  Created by Steven F. Daniel on 28/06/2018.
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using System;
using System.Threading.Tasks;
using TrackMyWalks.Services;
using Xamarin.Forms;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System.Diagnostics;

[assembly: Dependency(typeof(LocationService))]
namespace TrackMyWalks.Services
{
    public class LocationService : ILocationService
    {
        // Declare our EventHandler that can be referenced within the App
        public event EventHandler<PositionEventArgs> LocationChanged;

        // Retrieves the current GPS Coordinates for the device
        public async Task<Position> GetCurrentPosition()
        {
            Position position = null;
            try
            {
                // Initialise our current location and set the accuracy in Meters
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 200;

                // Check and get a cached position if we have one
                position = await locator.GetLastKnownLocationAsync();
                if (position != null) return position;

                // Check to see if Location Services are available / enabled
                if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
                {
                    return null;
                }
                // Call the GetPositionAsync to retrieve the GPS Coordinates
                position = await locator.GetPositionAsync(TimeSpan.FromSeconds(1), null, true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("There was a problem getting the location: " + ex);
            }

            // Return the current location coordinates
            return position;
        }

        // Asynchronously listens for changes in GPS location updates
        public async Task StartListening()
        {
            // Check to see if we are currently listening for updates
            if (CrossGeolocator.Current.IsListening)
                return;

            // Check what Target OS Platform we are running on whenever the app starts
            if (Device.RuntimePlatform.Equals(Device.Android))
            {
                await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(1), 100, true);
            }
            else
            {
                // Start listening for changes in location within the Background for iOS
                await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(1), 100, true, new ListenerSettings
                {
                    ActivityType = ActivityType.AutomotiveNavigation,
                    AllowBackgroundUpdates = true,
                    DeferLocationUpdates = true,
                    DeferralDistanceMeters = 500, // 1
                    DeferralTime = TimeSpan.FromSeconds(1),
                    ListenForSignificantChanges = false,  // true
                    PauseLocationUpdatesAutomatically = false
                });
            }

            // EventHandler to determine whenever the GPS position changes
            CrossGeolocator.Current.PositionChanged += (sender, e) =>
            {
                // Raise our LocationChanged EventHandler, using the Coordinates
                LocationChanged.Invoke(sender, e);
            };
        }

        // Stops listening for location service updates on the device
        public async void StopListening()
        {
            // Checks to see if we are currently listening for updates
            if (!CrossGeolocator.Current.IsListening)
                return;

            // Stops listening for updates, and removes our LocationChanged EventListener
            await CrossGeolocator.Current.StopListeningAsync();
            CrossGeolocator.Current.PositionChanged -= LocationChanged;
        }
    }
}