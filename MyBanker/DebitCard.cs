using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBanker
{
    public class DebitCard : Card
    {
        public DebitCard(Customer customer) : base(customer)
        {
            RequiredAge = 0;
            Prefixes = new List<int>() { 2400 };
            CardNumber = GenerateCardNumber();
        }
    }
}
