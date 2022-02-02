using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBanker
{
    public class Maestro : Card, IExpiryDate, INonDomesticTransaction
    {
        public string ExpiryDate { get; set; }

        public Maestro(Customer customer) : base(customer)
        {
            RequiredAge = 18;
            Prefixes = new List<int>() { 5018, 5020, 5038, 5893, 6304, 6759, 6761, 6762, 6763 };
            CardNumberLength = 19;
            CardNumber = GenerateCardNumber();

            ExpiryDate = CalculateExpiryDate(5, 8);
        }

        /// <summary>
        /// Simulates international transaction.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public string InternationalTransaction(int amount, string currency)
        {
            // Get output from interface
            if (amount <= this.Customer.Account.Balance)
            {
                this.Customer.Account.Balance -= amount;
                return (this as INonDomesticTransaction).InternationalTransactionOutput(amount, currency);
            }
            else
            {
                return $"Du har ikke nok penge tilgængelig på kontoen.\nTilgængelig saldo: {this.Customer.Account.Balance}";
            }
            
        }

        /// <summary>
        /// Simulates online transaction
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public string OnlineTransaction(int amount, string currency)
        {
            // Get output from interface
            if (amount <= this.Customer.Account.Balance)
            {
                this.Customer.Account.Balance -= amount;
                return (this as INonDomesticTransaction).OnlineTransactionOutput(amount, currency);
            }
            else
            {
                return $"Du har ikke nok penge tilgængelig på kontoen.\nTilgængelig saldo: {this.Customer.Account.Balance}";
            }
        }

        /// <summary>
        /// Calculates expiry date in MM/yy format
        /// </summary>
        /// <param name="years">Years until the card expires</param>
        /// <param name="months">Months until the card expires</param>
        /// <returns></returns>
        public string CalculateExpiryDate(int years, int months)
        {
            DateTime startDate = DateTime.Now; // Time that the card is issued
            DateTime addYears = startDate.AddYears(years); // The card expires after a number of months not divisible by 12, so first we add each full year
            DateTime endDate = addYears.AddMonths(months); // Then we add the additional months

            // Return expiry date in correct format
            return endDate.ToString("MM/yy");
        }
    }
}
