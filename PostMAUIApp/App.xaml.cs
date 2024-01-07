using System;
using PostMAUIApp.Data;
using System.IO;

namespace PostMAUIApp
{
    public partial class App : Application
    {
        static PackageDatabase database;

        public static PackageDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new PackageDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Package.db3"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
