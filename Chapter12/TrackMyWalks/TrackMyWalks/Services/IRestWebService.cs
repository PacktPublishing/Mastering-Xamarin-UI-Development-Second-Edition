//
//  IRestWebService.cs
//  RESTWebService Interface used by our Rest WebService Class
//
//  Created by Steven F. Daniel on 06/08/2018.
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyWalks.Models;

namespace TrackMyWalks.Services
{
    public interface IRestWebService
    {
        // Gets all of the Walk Entries from our database.
        Task<List<WalkDataModel>> GetWalkEntries();

        // Saves our Walk Entry to the database.
        Task SaveWalkEntry(WalkDataModel item, bool isAdding);

        // Deletes a specific Walk Entry from the database.
        Task DeleteWalkEntry(string id);
    }
}