using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IAuthInterface
    {
        string SignIn (string username, string password);
        string GetData(string jwtToken);
    }
}
