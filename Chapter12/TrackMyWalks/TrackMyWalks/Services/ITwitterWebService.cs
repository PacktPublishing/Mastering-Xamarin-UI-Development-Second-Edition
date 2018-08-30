//
//  ITwitterWebService.cs
//  TwitterWebService Interface used by our TwitterWebService Class
//
//  Created by Steven F. Daniel on 10/08/2018.
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Xamarin.Auth;

namespace TrackMyWalks.Services
{
    public interface ITwitterWebService
    {
        // Instance method to get the user's Twitter Profile Details
        Task<JObject> GetTwitterProfile(Account e);

        // Instance method to post a Tweet message to the users Twitter Feed
        Task<string> TweetMessage(string message, Account e);
    }
}