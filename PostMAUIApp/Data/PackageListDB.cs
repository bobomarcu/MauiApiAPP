using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using PostMAUIApp.Models;

namespace PostMAUIApp.Data
{
    public class PackageDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public PackageDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Package>().Wait();
        }

        public Task<List<Package>> GetPackagesAsync()
        {
            return _database.Table<Package>().ToListAsync();
        }

        public Task<Package> GetPackageAsync(int id)
        {
            return _database.Table<Package>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SavePackageAsync(Package package)
        {
            if (package.ID != 0)
            {
                return _database.UpdateAsync(package);
            }
            else
            {
                return _database.InsertAsync(package);
            }
        }

        public Task<int> DeletePackageAsync(Package package)
        {
            return _database.DeleteAsync(package);
        }
    }
}
