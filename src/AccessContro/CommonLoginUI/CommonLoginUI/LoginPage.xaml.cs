using Microsoft.IdentityModel.Tokens;
using Microsoft.Rest;
using ServiceReference1;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace CommonLoginUI;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    protected override bool OnBackButtonPressed()
    {
        Application.Current.Quit();
        return true;
    }

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        
        if (IsCredentialCorrect(Username.Text, Password.Text))
        {
            await SecureStorage.SetAsync("hasAuth", "true");
            
            var tokenClient = new ServiceReference1.JwtTokenClient();           

            var token = await tokenClient.GenerateJwtTokenAsync(Username.Text, Password.Text);
            bool isAuthorize = string.IsNullOrEmpty(token) ? false : true;            

            User.currentUserRole = User.GetUserList().Where(x => x.Name == Username.Text).Select(x => x.Role).Single();

            User.currentUserpermission = User.GetUserList().Where(x => x.Name == Username.Text).Select(x => x.UserPermission).Single();

            User.currentAppAccess = User.GetUserList().Where(x => x.Name == Username.Text).Select(x => x.UserApplicationAccess).Single();
            
            List<string> rolePermission = new List<string> { "admin", "system", "staff" };
                        
            if(isAuthorize && (User.currentAppAccess== ApplicationAccess.accesscontrol) && rolePermission.Contains(User.currentUserRole.ToString()))
            {                
                await Shell.Current.GoToAsync("///home");
            }            
            else
            {
                await DisplayAlert("Access Denied", "User doesnt have access rights to this system", "Ok");
            }
        }
        else
        {
            await DisplayAlert("Login failed", "Uusername or password if invalid", "Try again");
        }
    }    

    bool IsCredentialCorrect(string username, string password)
    {
        //return Username.Text == "admin" && Password.Text == "1234";
        List<User> users = User.GetUserList();
        foreach (User user in users)
        {
            if (user.Name == username && user.Password == password)
                return true;
        }
        return false;

    }    
}