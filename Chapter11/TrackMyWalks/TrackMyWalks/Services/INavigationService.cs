//
//  INavigationService.cs
//  Navigation Service Interface that each of our ViewModels will use
//
//  Created by Steven F. Daniel on 16/06/2018.
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using System;
using System.Threading.Tasks;
using TrackMyWalks.ViewModels;
using Xamarin.Forms;

namespace TrackMyWalks.Services
{
    public interface INavigationService
    {
        // Asynchronously removes the most recent page from the navigation stack.
        Task<Page> RemoveViewFromStack();

        // Returns to the Root Page after removing the current page from the navigation stack
        Task BackToMainPage();

        // Navigate to a particular ViewModel within our MVVM Model
        Task NavigateTo<TVM>() where TVM : BaseViewModel;
    }
}