//
//  WalkEntryPage.xaml.cs
//  Data Entry screen that allows new walk information to be added 
//
//  Created by Steven F. Daniel on 14/05/2018
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using System;
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
            BindingContext = new WalkEntryPageViewModel();
            SetBinding(TitleProperty, new Binding(BaseViewModel.PageTitlePropertyName));
        }

        // Instance method that saves the new walk entry
        public async void SaveWalkItem_Clicked(object sender, EventArgs e)
        {
            // Prompt the user with a confirmation dialog to confirm
            if (await DisplayAlert("Save Walk Entry Item", "Proceed and save changes?", "OK", "Cancel"))
            {
                // Attempt to save and validate our Walk Entry Item
                if (!_viewModel.ValidateFormDetailsAndSave())
                    // Error Saving - Must have Title and description
                    await DisplayAlert("Validation Error", "Title and Description are required.", "OK");
                else
                    // Navigate back to the Track My Walks Listing page
                    await Navigation.PopToRootAsync(true);
            }
            else
            {
                // Navigate back to the Track My Walks Listing page
                await Navigation.PopToRootAsync(true);
            }
        }
    }
}