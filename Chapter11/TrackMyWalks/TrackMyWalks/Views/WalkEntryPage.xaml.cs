//
//  WalkEntryPage.xaml.cs
//  Data Entry screen that allows new walk information to be added 
//
//  Created by Steven F. Daniel on 14/05/2018
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using System;
using TrackMyWalks.Services;
using TrackMyWalks.ViewModels;
using Xamarin.Forms;

namespace TrackMyWalks.Views
{
    public partial class WalkEntryPage : ContentPage
    {
        // Return the Binding Context for the ViewModel
        WalkEntryPageViewModel _viewModel => BindingContext as WalkEntryPageViewModel;

        public WalkEntryPage()
        {
            InitializeComponent();

            // Update the Title and Initialise our BindingContext for the Page
            Title = "New Walk Entry Page";
            BindingContext = new WalkEntryPageViewModel(DependencyService.Get<INavigationService>());
            SetBinding(TitleProperty, new Binding(BaseViewModel.PageTitlePropertyName));
        }

        // Instance method that saves the new walk entry
        public async void SaveWalkItem_Clicked(object sender, EventArgs e)
        {
            // Prompt the user with a confirmation dialog to confirm
            if (await DisplayAlert("Save Walk Entry Item",
                                   "Proceed and save changes?",
                                   "OK", "Cancel"))
            {
                // Attempt to save and validate our Walk Entry Item
                if (!await _viewModel.ValidateFormDetailsAndSave())
                {
                    // Error Saving - Must have Title, Description, and Image URL
                    await DisplayAlert("Validation Error",
                                       "Title, Description and Image URL are required.",
                                       "OK");
                }
                else
                {
                    // Navigate back to the Track My Walks Listing page
                    await _viewModel.Navigation.RemoveViewFromStack();
                }
            }
            else
            {
                // Navigate back to the Track My Walks Listing page
                await _viewModel.Navigation.RemoveViewFromStack();
            }
        }

        // Method to initialise our View Model when the ContentPage appears
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Create a Simple Animation to rotate our Difficulty Level Image
            DifficultyLevel.AnchorY = (Math.Min(DifficultyLevel.Width, DifficultyLevel.Height) / 2) / DifficultyLevel.Height;
            await DifficultyLevel.RotateTo(360, 2000, Easing.BounceOut);

            // Create a SwingingEntrance Animation for our WalkDetails TableView
            WalkDetails.RotationY = 180;
            await WalkDetails.RotateYTo(0, 1000, Easing.BounceOut);
            WalkDetails.AnchorX = 0.5;
        }
    }
}