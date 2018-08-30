//
//  TwitterAuthDetails.cs
//  Twitter class that will store Twitter Authentication Tokens
//
//  Created by Steven F. Daniel on 10/08/2018
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using Newtonsoft.Json.Linq;
using Xamarin.Auth;

namespace TrackMyWalks.Services
{
    public class TwitterAuthDetails
    {
        // Property to store the currently logged in user
        public static bool isLoggedIn => !string.IsNullOrWhiteSpace(AuthToken);

        // Declare and define your Twitter Consumer Key
        public static string ConsumerKey => "YOUR_CONSUMER_API_KEY";
        public static string ConsumerSecret => "YOUR_CONSUMER_API_KEY_SECRET";

        // Declare a property to get our Twitter User Details
        static JObject _userDetails;
        public static JObject UserDetails => _userDetails;

        // Instance method to store our Twitter User Details
        public static void StoreUserDetails(JObject userDetails)
        {
            _userDetails = userDetails;
        }

        // Property to get our Twitter Authentication Token
        static string _authToken;
        public static string AuthToken => _authToken;

        // Instance method to store our Twitter Auth Token
        public static void StoreAuthToken(string authToken)
        {
            _authToken = authToken;
        }

        // Property to get our Twitter Authentication Token Secret
        static string _authTokenSecret;
        public static string AuthTokenSecret => _authTokenSecret;

        // Instance method to store our Twitter Auth Token Secret
        public static void StoreTokenSecret(string authTokenSecret)
        {
            _authTokenSecret = authTokenSecret;
        }

        // Property to get our Twitter Authentication Account Details
        static Account _authAccount;
        public static Account AuthAccount => _authAccount;

        // Instance method to store our Twitter Authentication Account Details
        public static void StoreAccountDetails(Account authAccount)
        {
            _authAccount = authAccount;
        }
    }
}