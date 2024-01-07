using PostAppMaui.Data;


namespace PostAppMaui
{
    public partial class App : Application
    {
        public static PackageDatabase Database { get; private set; }
        public App()
        {
            Database = new PackageDatabase(new RestService());
            MainPage = new NavigationPage(new ListEntryPage());

            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
