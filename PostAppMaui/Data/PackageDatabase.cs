using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostApplication.Models;

namespace PostAppMaui.Data
{
    public class PackageDatabase
    {
        IRestService restService;

        public PackageDatabase(IRestService service)
        {
            restService = service;
        }

        public Task<List<Package>> GetPackagesAsync()
        {
            return restService.RefreshDataAsync();
        }

        public Task SavePackageAsync(Package item, bool isNewItem = true)
        {
            return restService.SavePackageAsync(item, isNewItem);
        }

        public Task UpdatePackageStatusAsync(int packageId, string newStatus)
        {
            return restService.UpdatePackageStatusAsync(packageId, newStatus);
        }

        public Task DeletePackageAsync(Package item)
        {
            return restService.DeletePackageAsync(item.ID);
        }
    }
}
