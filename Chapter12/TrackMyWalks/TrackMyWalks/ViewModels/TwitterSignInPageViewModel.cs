//
//  TwitterSignInPageViewModel.cs
//  The ViewModel for our TwitterSignInPage ContentPage
//
//  Created by Steven F. Daniel on 10/08/2018.
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using System.Threading.Tasks;
using TrackMyWalks.Services;

namespace TrackMyWalks.ViewModels
{
    public class TwitterSignInPageViewModel : BaseViewModel
    {
        public TwitterSignInPageViewModel(INavigationService navService) : base(navService)
        {
        }
        // Instance method to initialise the TwitterSignInPageViewModel
        public override async Task Init()
        {
            await Task.Factory.StartNew(() =>
            {
            });
        }
    }
}