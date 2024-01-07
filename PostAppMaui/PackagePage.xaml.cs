namespace PostAppMaui;
using PostApplication.Models;

public partial class PackagePage : ContentPage
{
	public PackagePage()
	{
		InitializeComponent();
	}
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var package = (Package)BindingContext;
        await App.Database.SavePackageAsync(package);
        await Navigation.PopAsync();
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var package = (Package)BindingContext;
        await App.Database.DeletePackageAsync(package);
        await Navigation.PopAsync();
    }
}