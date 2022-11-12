namespace AuthenticationWithReactAndDotnetwebapiWebAPI.Models
{
    public class UserConstants
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel() { 
                UserName = "manny",
                EmailAddress = "e@jmail.com",
                Password="pass1",
                GivenName="Emmanuel",
                Surname="lartey",
                Role="Administrator"},
            new UserModel() { 
                UserName = "manny1",
                EmailAddress = "e1@jmail.com",
                Password="pass1",
                Surname="lartey",
                GivenName="Emmanuel1",
                Role="User"},
        };
    }
}
