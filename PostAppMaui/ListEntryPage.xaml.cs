namespace PostAppMaui;

using Plugin.LocalNotification;
using PostApplication.Models;
using System.Net;

public partial class ListEntryPage : ContentPage
{

	public ListEntryPage()
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