using AuditManagementSystem.Views;
using CommonLoginUI;

namespace AuditManagementSystem
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("login", typeof(LoginPage));
            Routing.RegisterRoute("audit", typeof(AuditDetailsPage));
            Routing.RegisterRoute("settings", typeof(SettingsPage));
        }
    }
}