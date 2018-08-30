//
//  WalkTrailInfoPage.xaml.cs
//  Displays related trail information chosen from the WalksMainPage
//
//  Created by Steven F. Daniel on 14/05/2018
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using System;
using Xamarin.Forms;
using TrackMyWalks.ViewModels;
using TrackMyWalks.Services;
using System.Threading.Tasks;

namespace TrackMyWalks.Views
{
    public partial class WalkTrailInfoPage : ContentPage
    {
        // Return the Binding Context for the ViewModel
        WalkTrailInfoPageViewModel _viewModel => BindingContext as WalkTrailInfoPageViewModel;

        public WalkTrailInfoPage()
        {
            InitializeComponent();

            // Update the Title and Initialise our BindingContext for the Page
            this.Title = "Trail Walk Information";
            this.BindingContext = new WalkTrailInfoPageViewModel(DependencyService.Get<INavigationService>());
        }

        // Method to initialise our View Model when the ContentPage appears
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Create a SlidingEntrance Animation for WalkTrailInfoPage
            double offset = 1000;
            foreach (View view in TrailInfoScrollView.Children)
            {
                view.TranslationX = offset;
                offset *= -1;

                await Task.WhenAny(view.TranslateTo(0, 0, 1000, Easing.SpringOut),
                                 Task.Delay(100));
            }

            // Create a Custom Animation for our BeginTrailWalk button
            var animation = new Animation(v =>
                            BeginTrailWalk.BackgroundColor =
                            Color.FromHsla(v, 1, 0.5),
                            start: 0, end: 1);

            animation.Commit(this, "BeginWalkCustomAnimation",
                             16, 5000, Easing.Linear, (v, c) =>
                             BackgroundColor = Color.Default,
                             () => true);
        }

        // Instance method that proceeds to begin a new walk trail
        public async void BeginTrailWalk_Clicked(object sender, EventArgs e)
        {
            if (App.SelectedItem == null)
                return;

            // Create and Apply an Easing Function to our Button
            await BeginTrailWalk.RotateTo(15, 1000, new Easing(t =>
            Math.Sin(Math.PI * t) *
            Math.Sin(Math.PI * 20 * t)));

            await _viewModel.Navigation.NavigateTo<WalkDistancePageViewModel>();
        }
    }
}