//
//  ButtonShadowEffect.cs
//  Creates a custom Button Shadow Effect using PlatformEffects (iOS)
//
//  Created by Steven F. Daniel on 16/07/2018.
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("GeniesoftStudios")]
[assembly: ExportEffect(typeof(TrackMyWalks.iOS.CustomEffects.ButtonShadowEffect), "ButtonShadowEffect")]
namespace TrackMyWalks.iOS.CustomEffects
{
    public class ButtonShadowEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                Container.Layer.ShadowOpacity = 0.5f;
                Container.Layer.ShadowColor = UIColor.Black.CGColor;
                Container.Layer.ShadowRadius = 2;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: " + ex.Message);
            }
        }

        protected override void OnDetached()
        {
            Container.Layer.ShadowOpacity = 0;
        }
    }
}