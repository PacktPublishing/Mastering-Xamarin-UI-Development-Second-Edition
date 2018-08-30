//
//  CustomMapRenderer.cs
//  Draws an overlay onto a Custom Native Map that maps out the route taken
//
//  Created by Steven F. Daniel on 28/06/2018
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using Android.Content;
using Android.Gms.Maps.Model;
using TrackMyWalks.Droid;
using TrackMyWalks.Views.MapOverlay;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;

[assembly: ExportRenderer(typeof(CustomMapOverlay), typeof(CustomMapRenderer))]
namespace TrackMyWalks.Droid
{
    public class CustomMapRenderer : MapRenderer
    {
        CustomMapOverlay formsMap;

        public CustomMapRenderer(Context context) : base(context)
        {
        }

        // Redraw the map whenever the RouteCoordinates property has changed to draw the line from PointA to PointB
        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                formsMap = (CustomMapOverlay)e.NewElement;
                Control.GetMapAsync(this);
            }
        }

        // Customize the rendering of our Polyline overlay within the map
        protected override void OnMapReady(Android.Gms.Maps.GoogleMap map)
        {
            base.OnMapReady(map);

            var polylineOptions = new PolylineOptions();
            polylineOptions.InvokeColor(0x66FF0000);

            // Extract each position from our RouteCoordinates List
            foreach (var position in formsMap.RouteCoordinates)
            {
                // Add each Latitude and Longitude position to our PolylineOptions
                polylineOptions.Add(new LatLng(position.Latitude, position.Longitude));
            }

            // Finally, add the Polyline to our map
            NativeMap.AddPolyline(polylineOptions);
        }
    }
}