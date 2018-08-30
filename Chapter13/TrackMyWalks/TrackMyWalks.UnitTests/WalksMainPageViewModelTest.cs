//
//  WalksMainPageViewModelTest.cs
//  Unit Test of the WalksMainPageViewModel
//
//  Created by Steven F. Daniel on 14/08/2018
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using System.Threading.Tasks;
using Moq;
using TrackMyWalks.Services;
using TrackMyWalks.ViewModels;
using Xunit;

namespace TrackMyWalks.UnitTest
{
    public class WalksMainPageViewModelTest
    {
        [Fact]
        public async Task CheckIfWalkEntryIsNotNull()
        {
            var navMock = new Mock<INavigationService>().Object;
            var viewModel = new WalksMainPageViewModel(navMock);

            // Arrange 
            viewModel.WalksListModel = null;

            // Act
            await viewModel.GetWalkTrailItems();

            // Assert
            Assert.NotNull(viewModel.WalksListModel);
        }
    }
}