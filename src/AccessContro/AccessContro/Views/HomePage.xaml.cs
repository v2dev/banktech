using CommonLoginUI;

namespace AccessContro.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    private void LoginButton_Clicked(object sender, EventArgs e)
    {

        List<string> rolePermission = new List<string> { "admin", "system" };

        if (rolePermission.Contains(User.currentUserRole.ToString()))
        {
            
            Shell.Current.GoToAsync("grid");
        }
        else
        {
            DisplayAlert("Access Denied", "User doesnt have access rights to this page", "Sorry");
        }              

    }

    
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }  
}