namespace PostMAUIApp;
using PostMAUIApp.Models;
using PostMAUIApp.Data;


public partial class EntryListPage : ContentPage
{
	public EntryListPage()
	{
		InitializeComponent();

	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        listView.ItemsSource = await App.Database.GetPackagesAsync();
    }

    async void OnPackageAddedClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PackagePage
        {
            BindingContext = new Package()
        });
    }

    async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            await Navigation.PushAsync(new PackagePage
            {
                BindingContext = e.SelectedItem as Package
            });
        }
    }
}