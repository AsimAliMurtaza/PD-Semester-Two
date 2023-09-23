using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessApp
{
    internal class User
    {
        public string usernames;
        public string passwords;
        public string emails;
        public string roles;
        public List<Product> products = new List<Product>();
        public User(string u, string p)
        {
            usernames = u;
            passwords = p;
        }
        public User(string u, string p, string e, string r)
        {
            usernames = u;
            passwords = p;
            emails = e;
            roles = r;
        }

        public User signIn(string u, string p, List<User> userInfo)
        {
            foreach (var user in userInfo)
            {
                if (u == user.usernames && p == user.passwords)
                {
                    return user;
                }
            }
            return null;
        }

        public bool signUpStore(ref User signUpData, List<User> userData)
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
        public void addProductCart(Product product)
        {
            products.Add(product);
        }
        public void removeProductCart(Product product)
        {
            products.Remove(product);
        }
    }
}
