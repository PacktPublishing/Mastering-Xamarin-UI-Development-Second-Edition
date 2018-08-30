//
//  TwitterSignInPage.xaml.cs
//  Displays the Twitter Sign In Page using the Twitter API
//
//  Created by Steven F. Daniel on 10/08/2018
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using TrackMyWalks.Services;
using TrackMyWalks.ViewModels;
using Xamarin.Forms;

namespace TrackMyWalks.Views
{
    public partial class TwitterSignInPage : ContentPage
    {
        // Return the Binding Context for the ViewModel
        TwitterSignInPageViewModel _viewModel => BindingContext as TwitterSignInPageViewModel;

        public TwitterSignInPage()
        {
            InitializeComponent();

            // Update the Title and Initialise our BindingContext for the Page
            this.Title = "Track My Walks Twitter Sign In";
            this.BindingContext = new TwitterSignInPageViewModel(DependencyService.Get<INavigationService>());
        }

        // Method to initialise our View Model when the ContentPage appears
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Check to see if we have logged in and remove our Twitter Sign In Page
            if (_viewModel != null && TwitterAuthDetails.isLoggedIn)
            {
                // Pops our Twitter Sign In Page from our Navigation Stack
                await Navigation.PopAsync();
            }
        }
    }
}