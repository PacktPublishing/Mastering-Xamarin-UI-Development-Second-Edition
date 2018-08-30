//
//  ButtonShadowEffect.cs
//  Creates a custom Button Shadow Effect using PlatformEffects (Android)
//
//  Created by Steven F. Daniel on 16/07/2018.
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System;

[assembly: ResolutionGroupName("GeniesoftStudios")]
[assembly: ExportEffect(typeof(TrackMyWalks.Droid.CustomEffects.ButtonShadowEffect), "ButtonShadowEffect")]
namespace TrackMyWalks.Droid.CustomEffects
{
    public class ButtonShadowEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var control = Control as Android.Widget.Button;
                Android.Graphics.Color color = Android.Graphics.Color.Red;
                control.SetShadowLayer(12, 4, 4, color);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: " + ex.Message);
            }
        }

        protected override void OnDetached()
        {
            throw new NotImplementedException();
        }
    }
}