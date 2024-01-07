using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PostApplication.Models;

namespace PostAppMaui.Data
{
    public class RestService : IRestService
    {
        HttpClient client;

        // Modify this URL with the appropriate IP and port
        string RestUrl = "https://192.168.100.97:45456/api/Packages/{0}";

        public List<Package> Items { get; private set; }

        public RestService()
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
                (message, cert, chain, errors) => { return true; };
            client = new HttpClient(httpClientHandler);
        }

        public async Task<Package> GetPackageDetailsAsync(int id)
        {
            Package package = null;
            Uri uri = new Uri(string.Format(RestUrl, id));

            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    package = JsonConvert.DeserializeObject<Package>(content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return package;
        }
        public async Task<List<Package>> RefreshDataAsync()
        {
            List<Package> packages = new List<Package>();
            Uri uri = new Uri(RestUrl);

            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    packages = JsonConvert.DeserializeObject<List<Package>>(content);

                    foreach (var package in packages)
                    {
                        Package detailedPackage = await GetPackageDetailsAsync(package.ID);
                        if (detailedPackage != null)
                        {
                            package.Sender = detailedPackage.Sender;
                            package.Receiver = detailedPackage.Receiver;
                            package.AssignedCourier = detailedPackage.AssignedCourier;
                            package.PostOffice = detailedPackage.PostOffice;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return packages;
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
    }
}
