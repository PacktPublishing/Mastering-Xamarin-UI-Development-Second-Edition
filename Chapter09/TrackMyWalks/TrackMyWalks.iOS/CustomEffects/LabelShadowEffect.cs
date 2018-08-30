//
//  LabelShadowEffect.cs
//  Creates a custom Label Shadow Effect using PlatFormEffects (iOS)
//
//  Created by Steven F. Daniel on 16/07/2018.
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using System;
using CoreGraphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(TrackMyWalks.iOS.CustomEffects.LabelShadowEffect), "LabelShadowEffect")]
namespace TrackMyWalks.iOS.CustomEffects
{
    public class LabelShadowEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                Control.Layer.CornerRadius = 5;
                Control.Layer.ShadowColor = Color.Black.ToCGColor();
                Control.Layer.ShadowOffset = new CGSize(4, 4);
                Control.Layer.ShadowOpacity = 0.5f;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }

        protected override void OnDetached()
        {
        }
    }
}