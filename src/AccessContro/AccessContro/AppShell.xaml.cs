using AccessContro.Views;
using CommonLoginUI;

namespace AccessContro;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
      

        //Register all routes
        Routing.RegisterRoute("login", typeof(LoginPage));
        Routing.RegisterRoute("main", typeof(MainPage));
        Routing.RegisterRoute("home", typeof(HomePage));
        Routing.RegisterRoute("settings", typeof(SettingsPage));
        Routing.RegisterRoute("grid", typeof(GridView));
      
    }
}
