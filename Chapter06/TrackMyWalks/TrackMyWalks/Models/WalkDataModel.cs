//
//  WalkDataModel.cs
//  Data Model that will store Walk Trail Information
//
//  Created by Steven F. Daniel on 14/05/2018
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using System;

namespace TrackMyWalks.Models
{
    public class WalkDataModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Distance { get; set; }
        public string Difficulty { get; set; }
        public string ImageUrl { get; set; }
    }
}