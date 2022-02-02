using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBanker
{
    public class VisaElectron : Card, IExpiryDate, INonDomesticTransaction, ITransactionLimit
    {
        public string ExpiryDate { get; set; }
        public int TransactionLimit { get; set; }

        public VisaElectron(Customer customer) : base(customer)
        {
            RequiredAge = 15;
            Prefixes = new List<int>() { 4026, 417500, 4508, 4844, 4913, 4917 };
            TransactionLimit = 10000;
            ExpiryDate = CalculateExpiryDate(5, 0);
            CardNumber = GenerateCardNumber();
        }

        public override string DomesticTransaction(int amount)
        {
            if ((amount <= this.Customer.Account.Balance) && (this.Customer.Account.SpentThisMonth + amount <= this.TransactionLimit))
            {
                // Add spent amount to customers 'spent this month' amount, and subtract it from the balance on his account
                this.Customer.Account.SpentThisMonth += amount;
                this.Customer.Account.Balance -= amount;
                return base.DomesticTransaction(amount);
            } 
            else
            {
                return $"Overførsel på {amount} kroner kunne ikke gennemføres, da denne ville overstige din månedlige grænse på {TransactionLimit} kroner.";
            }
        }

        public string InternationalTransaction(int amount, string currency)
        {
            if ((amount <= this.Customer.Account.Balance) && (this.Customer.Account.SpentThisMonth + amount <= this.TransactionLimit))
            {
                // Add spent amount to customers daily spent amount
                this.Customer.Account.SpentThisMonth += amount;

                // Subtract amount from the balance on this account
                this.Customer.Account.Balance -= amount;

                // Call method from interface
                return (this as INonDomesticTransaction).InternationalTransactionOutput(amount, currency);
            }
            else
            {
                // Transaction failed
                return $"Overførsel på {amount} {currency} kunne ikke gennemføres, da denne ville oversige din månedlige begrænsning på {TransactionLimit} kroner.";
            }
        }

        public string OnlineTransaction(int amount, string currency)
        {
            if ((amount <= this.Customer.Account.Balance) && (amount <= this.TransactionLimit)) // TODO: Check for how much customer has already spent
            {
                // Add spent amount to customers 'spent this month' amount, and subtract it from the balance on his account
                this.Customer.Account.SpentThisMonth += amount;
                this.Customer.Account.Balance -= amount;
                // Call method from interface
                return (this as INonDomesticTransaction).OnlineTransactionOutput(amount, currency);
            }
            else
            {
                // Transaction failed
                return $"Overførsel på {amount} {currency} kunne ikke gennemføres, da denne ville oversige din månedlige begrænsning på {TransactionLimit} kroner.";
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
