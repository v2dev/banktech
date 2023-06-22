
using CommonLoginUI;
using System.Data;

namespace AccessContro.Views;
public partial class GridView : ContentPage
{   
    public GridView()
	{
		InitializeComponent();        
    }    

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }

    protected override void OnAppearing()
    {        
        if (User.currentUserpermission == UserPermission.onlyread)
        {
            this.InputTransparent = true;
        }
    }
}