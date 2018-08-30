//
//  RestWebService.cs
//  RESTWebService Class that will be used to handle performing of CRUD operations
//
//  Created by Steven F. Daniel on 06/08/2018.
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TrackMyWalks.Models;

namespace TrackMyWalks.Services
{
    public class RestWebService : IRestWebService
    {
        // Declare our HttpClient manager object
        HttpClient client;

        // Declare our RestWebService Constructor
        public RestWebService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://trackmywalk.azurewebsites.net");
            client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");
        }

        // Retrieves all of the Walk Entries from our database.
        public async Task<List<WalkDataModel>> GetWalkEntries()
        {
            // Declare our WalkEntries Items List Collection to populate resultset
            var Items = new List<WalkDataModel>();
            try
            {
                var response = await client.GetAsync("tables/WalkEntries");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<WalkDataModel>>(content);
                }
            }
            catch (Exception ex)
            {
                // Catch and output any error messages that have occurred
                Debug.WriteLine("An error occurred {0}", ex.Message);
            }
            return Items;
        }

        // Saves the Walk Entry item that is currently being added/edited.
        public async Task SaveWalkEntry(WalkDataModel item, bool isAdding)
        {
            try
            {
                HttpResponseMessage responseMessage;
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Check to see if we are adding or editing, handle accordingly.
                if (isAdding)
                {
                    responseMessage = await client.PostAsync("tables/WalkEntries", content);
                }
                else
                {
                    responseMessage = await client.PutAsync("tables/WalkEntries", content);
                }
                // Check to see if we have successfully written the item to the database
                if (responseMessage.IsSuccessStatusCode)
                {
                    Debug.WriteLine("WalkEntry Item successfully saved.");
                }
            }
            catch (Exception ex)
            {
                // Catch and output any error messages that have occurred
                Debug.WriteLine("An error occurred {0}", ex.Message);
            }
        }

        // Deletes a specific Walk Entry from the database using the id.
        public async Task DeleteWalkEntry(string id)
        {
            try
            {
                var response = await client.DeleteAsync("/tables/WalkEntries/" + id);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("WalkEntry Item was successfully deleted.");
                }
            }
            catch (Exception ex)
            {
                // Catch and output any error messages that have occurred
                Debug.WriteLine("An error occurred {0}", ex.Message);
            }
        }
    }
}