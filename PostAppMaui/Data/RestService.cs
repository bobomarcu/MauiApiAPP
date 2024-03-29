﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PostApplication.Models;
using Microsoft.Extensions.Logging;

namespace PostAppMaui.Data
{
    public class RestService : IRestService
    {
        HttpClient client;
        private readonly ILogger<RestService> logger;

        string RestUrl = "https://192.168.100.97:45455/api/Packages/{0}";
        string RestUrlUpdate = "https://192.168.100.97:45455/api/Packages/{0}/updatestatus";
        public List<Package> Items { get; private set; }

        public RestService()
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            client = new HttpClient(httpClientHandler);
        }

        public async Task<List<Package>> RefreshDataAsync()
        {
            Items = new List<Package>();
            Uri uri = new Uri(string.Format(RestUrl, string.Empty));

            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<Package>>(content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Items;
        }

        public async Task SavePackageAsync(Package item, bool isNewItem = true)
        {
            Uri uri = new Uri(string.Format(RestUrl, string.Empty));

            try
            {
                string json = JsonConvert.SerializeObject(item);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                if (isNewItem)
                {
                    response = await client.PostAsync(uri, content);
                }
                else
                {
                    response = await client.PutAsync(uri, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(@"\tPackage successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public async Task DeletePackageAsync(int id)
        {
            Uri uri = new Uri(string.Format(RestUrl, id));

            try
            {
                HttpResponseMessage response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(@"\tPackage successfully deleted.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
        public async Task UpdatePackageStatusAsync(int id, string newStatus)
        {
            Uri uri = new Uri(string.Format(RestUrlUpdate, id));

            try
            {
                StringContent updatedContent = new StringContent($"\"{newStatus}\"", Encoding.UTF8, "application/json");

                HttpResponseMessage updateResponse = await client.PutAsync(uri, updatedContent);

                if (updateResponse.IsSuccessStatusCode)
                {
                    Console.WriteLine(@"\tPackage status successfully updated.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
    }
}
