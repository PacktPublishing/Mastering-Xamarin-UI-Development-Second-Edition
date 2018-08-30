//
//  LabelShadowEffect.cs
//  Creates a custom Label Shadow Effect using PlatformEffects (Android)
//
//  Created by Steven F. Daniel on 16/07/2018.
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(TrackMyWalks.Droid.CustomEffects.LabelShadowEffect), "LabelShadowEffect")]
namespace TrackMyWalks.Droid.CustomEffects
{
    public class LabelShadowEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var control = Control as Android.Widget.TextView;
                float radius = 5;
                float distanceX = 4;
                float distanceY = 4;
                Android.Graphics.Color color = Color.White.ToAndroid();
                control.SetShadowLayer(radius, distanceX, distanceY, color);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: " + ex.Message);
            }
        }

        protected override void OnDetached()
        {
        }
    }
}