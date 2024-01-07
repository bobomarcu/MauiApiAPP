using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using PostApplication.Models;


namespace PostAppMaui.Data
{
    public interface IRestService
    {
        Task<List<Package>> RefreshDataAsync();
        Task SavePackageAsync(Package item, bool isNewItem);
        Task DeletePackageAsync(int id);
    }
}
