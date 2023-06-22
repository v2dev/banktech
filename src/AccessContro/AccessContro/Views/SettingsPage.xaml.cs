using CommonLoginUI;
using Microsoft.Maui.Controls.Platform;
using System.Reflection;

namespace AccessContro.Views;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
		List<string> rolePermission = new List<string> { "admin", "system" };

		if (!rolePermission.Contains(User.currentUserRole.ToString()))
		{                   
            await Task.Delay(2000);
            await DisplayAlert("Access Denied", "User doesnt have access rights to this page", "Ok");
            await Shell.Current.Navigation.PopAsync();                        
        }        
    }
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
    private async void LogoutButton_Clicked(object sender, EventArgs e)
	{		
		if (await DisplayAlert("Are you sure?", "You will be logged out.", "Yes", "No"))
		{
			SecureStorage.RemoveAll();
			await Shell.Current.GoToAsync("///login");
		}
	}   
}