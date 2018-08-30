//
//  CustomMapOverlay.cs
//  Displays a custom map overlay using the stored Route Coordinates
//
//  Created by Steven F. Daniel on 28/06/2018
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace TrackMyWalks.Views.MapOverlay
{
    public class CustomMapOverlay : Map
    {
        public List<Position> RouteCoordinates { get; set; }

        public CustomMapOverlay()
        {
            RouteCoordinates = new List<Position>();
        }
    }
}