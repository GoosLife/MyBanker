using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBanker
{
    public class Mastercard : Card, INonDomesticTransaction, ICreditCard, ITransactionLimit, IExpiryDate, IDailyLimit
    {
        public int CreditLimit { get; set; }
        public int TransactionLimit { get; set; }
        public int DailyLimit { get; set; }
        public string ExpiryDate { get; set; }

        public Mastercard(Customer customer) : base(customer)
        {
            RequiredAge = 18;
            Prefixes = new List<int>() { 51, 52, 53, 54, 55 };
            ExpiryDate = CalculateExpiryDate(5, 0);
            TransactionLimit = 30000;
            CreditLimit = 40000;
            DailyLimit = 5000;
            CardNumber = GenerateCardNumber();
        }

        public override string DomesticTransaction(int amount)
        {
            if ((this.Customer.Account.SpentThisMonth + amount <= TransactionLimit && this.Customer.Account.SpentToday + amount <= DailyLimit) 
                && (amount <= this.Customer.Account.Balance + CreditLimit))
            {
                // Subtract spent amount from customers transaction limit as well as from the balance on his account
                this.Customer.Account.SpentThisMonth += amount;
                this.Customer.Account.SpentToday += amount;
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
            if ((this.Customer.Account.SpentThisMonth + amount <= TransactionLimit && this.Customer.Account.SpentToday + amount <= DailyLimit) 
                && (amount <= this.Customer.Account.Balance + CreditLimit))
            {
                // Subtract spent amount from customers transaction limit as well as from the balance on his account
                this.Customer.Account.SpentThisMonth += amount;
                this.Customer.Account.SpentToday += amount;
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
            if ((this.Customer.Account.SpentThisMonth + amount <= TransactionLimit && this.Customer.Account.SpentToday + amount <= DailyLimit) 
                && (amount <= this.Customer.Account.Balance + CreditLimit))
            {
                // Subtract spent amount from customers transaction limit as well as from the balance on his account
                this.Customer.Account.SpentThisMonth += amount;
                this.Customer.Account.SpentToday += amount;
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
