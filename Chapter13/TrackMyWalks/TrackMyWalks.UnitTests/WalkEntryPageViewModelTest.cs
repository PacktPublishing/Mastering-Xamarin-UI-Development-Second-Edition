//
//  WalkEntryPageViewModelTest.cs
//  Unit Test of the WalkEntryPageViewModel
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
    public class WalkEntryPageViewModelTest
    {
        [Fact]
        public async Task CheckIfEntryTitleIsEqual()
        {
            var navMock = new Mock<INavigationService>().Object;
            var viewModel = new WalkEntryPageViewModel(navMock);

            // Arrange 
            viewModel.Title = "New Walk Entry";

            // Act
            await viewModel.Init();

            // Assert
            Assert.Equal("New Walk Entry", viewModel.Title);
        }


        [Fact]
        public async Task CheckIfDifficultyIsEqual()
        {
            var navMock = new Mock<INavigationService>().Object;
            var viewModel = new WalkEntryPageViewModel(navMock);

            // Arrange
            viewModel.Difficulty = "Easy";

            // Act
            await viewModel.Init();

            // Assert
            Assert.Equal("Hard", viewModel.Difficulty);
        }

        [Fact]
        public async Task CheckIfDistanceIsNotEqual()
        {
            var navMock = new Mock<INavigationService>().Object;
            var viewModel = new WalkEntryPageViewModel(navMock);

            // Arrange
            viewModel.Distance = 256;

            // Act
            await viewModel.Init();

            // Assert
            Assert.NotEqual("0", viewModel.Difficulty);
        }
    }
}