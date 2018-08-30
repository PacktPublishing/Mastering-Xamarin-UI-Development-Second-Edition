//
//  ILocationService.cs
//  Location Service Interface used by our Location Service Class
//
//  Created by Steven F. Daniel on 28/06/2018.
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using System.Threading.Tasks;
using Plugin.Geolocator.Abstractions;

namespace TrackMyWalks.Services
{
    public interface ILocationService
    {
        // Asynchronously gets the current GPS location from the device.
        Task<Position> GetCurrentPosition();

        // Asynchronously listens for changes in the GPS coordinates
        Task StartListening();

        // Stops listening for changes in GPS location updates
        void StopListening();
    }
}