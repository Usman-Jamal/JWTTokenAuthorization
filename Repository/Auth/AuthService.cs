using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Repository
{
    public class AuthService: IAuthInterface
    {
        public AuthService() { 
        }
        public string SignIn(string username, string password)
        {
            if(username == null || password == null)
                throw new ArgumentNullException("username");
            if(password == null) throw new ArgumentNullException("password");
            if(username=="Admin" && password == "Admin@123")
            {
                Console.WriteLine("SignIn successfully");
                return username.GetToken();
            }
            return string.Empty;

        }
        public string GetData(string jwtToken)
        {
            var claims = jwtToken.GetClaim();
            if (claims != null) return claims.Id;
            return "UnAuthorized";
        }
    }
}
