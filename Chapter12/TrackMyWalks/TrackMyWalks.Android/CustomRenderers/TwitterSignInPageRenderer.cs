//
//  TwitterSignInPageRenderer.cs
//  TrackMyWalks Twitter SignIn Page (Android)
//
//  Created by Steven F. Daniel on 10/08/2018.
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using System;
using Android.App;
using Android.Content;
using TrackMyWalks.Droid;
using TrackMyWalks.Services;
using TrackMyWalks.Views;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(TwitterSignInPage), typeof(TwitterSignInPageRenderer))]
namespace TrackMyWalks.Droid
{
    public class TwitterSignInPageRenderer : PageRenderer
    {
        string oAuth_Token = String.Empty;
        string oAuth_Token_Secret = String.Empty;

        public TwitterSignInPageRenderer(Context context) : base(context)
        {
            var activity = context as Activity;

            var auth = new OAuth1Authenticator(
                consumerKey: TwitterAuthDetails.ConsumerKey,
                consumerSecret: TwitterAuthDetails.ConsumerSecret,
                requestTokenUrl: new Uri("https://api.twitter.com/oauth/request_token"),
                authorizeUrl: new Uri("https://api.twitter.com/oauth/authorize"),
                accessTokenUrl: new Uri("https://api.twitter.com/oauth/access_token"),
                callbackUrl: new Uri("https://mobile.twitter.com/home"));

            // Prevent displaying the Cancel button on the Twitter sign on page
            auth.AllowCancel = false;

            // Define our completion handler once the user has successfully signed in
            auth.Completed += async (object sender, AuthenticatorCompletedEventArgs e) =>
            {
                if (e.IsAuthenticated)
                {
                    e.Account.Properties.TryGetValue("oauth_token", out oAuth_Token);
                    e.Account.Properties.TryGetValue("oauth_token_secret", out oAuth_Token_Secret);

                    // Instantiate our class to Store our Twitter Authentication Token
                    TwitterAuthDetails.StoreAuthToken(oAuth_Token);
                    TwitterAuthDetails.StoreTokenSecret(oAuth_Token_Secret);
                    TwitterAuthDetails.StoreAccountDetails(e.Account);
                }

                // Remove our Twitter SignIn Page View from memory
                App.RemoveTwitterSignInPage();

                // Navigate to our Walks Main Page
                await App.NavigateToWalksMainPage();
            };
            activity.StartActivity(auth.GetUI(activity));
        }
    }
}