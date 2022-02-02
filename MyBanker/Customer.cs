using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBanker
{
    public class Customer
    {
        public Account Account { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Used to check if customer is eligible for cardtype.
        /// </summary>
        public int Age { get; set; }

        public Customer(string name, int age, Account account)
        {
            Name = name;
            Age = age;
            Account = account;
        }
    }
}
