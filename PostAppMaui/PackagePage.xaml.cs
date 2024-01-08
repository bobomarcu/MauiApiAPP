namespace PostAppMaui;

using Plugin.LocalNotification;
using PostApplication.Models;

public partial class PackagePage : ContentPage
{
	public PackagePage()
	{
		InitializeComponent();
	}
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var notification = new NotificationRequest
        {
            NotificationId = 1337,
            Title = "Package Update",
            Description = "A package has been updated",
            Schedule =
            {
                NotifyTime = DateTime.Now.AddSeconds(5)
            }
        };
        await LocalNotificationCenter.Current.Show(notification);
        var package = (Package)BindingContext;
        await App.Database.UpdatePackageStatusAsync(package.ID,package.Status);
        await Navigation.PopAsync();
    }


   
}