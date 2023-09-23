using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessApp
{
    public class Data
    {
        public string usernames;
        public string passwords;
        public string emails;
        public string roles;

        public Data(string u, string p) 
        {
            usernames = u;
            passwords = p;
        }
        public Data(string u, string p, string e, string r)
        {
            usernames = u;
            passwords = p;
            emails = e;
            roles = r;
        }

        public Data signIn(string u, string p, List<Data> userInfo)
        {
            foreach (var user in userInfo)
            {
                if (u == user.usernames  && p == user.passwords)
                {
                    return user;
                }
            }
            return null;
        }

        public bool signUpStore(ref Data signUpData, List<Data> userData)
        {
            if (userData.Count < 10)
            {
                userData.Add(signUpData);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
