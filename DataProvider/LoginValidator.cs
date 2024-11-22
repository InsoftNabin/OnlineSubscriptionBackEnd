using System;
using System.Collections.Generic;
using System.Text;

namespace DataProvider
{
    public class LoginValidator
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CustomerSubscriptionGuid { get; set; }
        public int Customer { get; set; }
        public int Product { get; set; }
    }
}
