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
        await App.Database.UpdatePackageStatusAsync(package.ID,package.Status);
        await Navigation.PopAsync();
    }


   
}