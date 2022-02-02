using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBanker
{
    public interface INonDomesticTransaction
    {
        public string InternationalTransaction(int amount, string currency);
        public string OnlineTransaction(int amount, string currency);

        public string InternationalTransactionOutput(int amount, string currency) // TODO: Actually convert currencies
        {
            return $"Du har brugt {amount} {currency} på en udenlandsk overførsel.";
        }

        public string OnlineTransactionOutput(int amount, string currency) // TODO: Actually convert currencies
        {
            return $"Du har brugt {amount} {currency} på nettet.";
        }
    }
}
