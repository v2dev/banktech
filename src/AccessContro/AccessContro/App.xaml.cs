using CommonLoginUI;

namespace AccessContro;

public partial class App : Application
{    
    public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
        
    }
}
