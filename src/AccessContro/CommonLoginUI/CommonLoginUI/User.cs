using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLoginUI
{
    public class User
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public ApplicationAccess UserApplicationAccess { get; set; }
        public UserPermission UserPermission { get; set; }       
        public static UserRole currentUserRole { get; set; }      
        public static UserPermission currentUserpermission { get; set; }
        public static ApplicationAccess currentAppAccess { get; set; }

        public static List<User> GetUserList()
        {
            List<User> list = new List<User>
            {
                new User { Name = "admin", Password = "1234", Role = UserRole.admin, UserPermission = UserPermission.full, UserApplicationAccess = ApplicationAccess.accesscontrol},
                new User { Name = "staff", Password = "1234", Role = UserRole.staff, UserPermission = UserPermission.onlyread,UserApplicationAccess = ApplicationAccess.accesscontrol},
                new User { Name = "system", Password = "1234", Role = UserRole.system, UserPermission = UserPermission.onlyread, UserApplicationAccess = ApplicationAccess.auditsystem}
            };

            return list;
        }        
    }

    public enum UserRole
    {
        admin,
        staff,
        system
    }
    public enum UserPermission
    {
        full,
        onlyread
    }

    public enum ApplicationAccess
    {
        full,
        accesscontrol,
        auditsystem
    }
   
}
