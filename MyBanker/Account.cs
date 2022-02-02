using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBanker
{
    public class Account
    {
        /// <summary>
        /// The registration number - represents the first 4 digits of the customers account number.
        /// It is stored as a string, but must be digits.
        /// </summary>
        static string RegistrationNumber = "3520";

        /// <summary>
        /// The account number associated with the customer.
        /// TODO: What if customer has multiple accounts?
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Balance on the account
        /// </summary>
        public int Balance { get; set; }

        /// <summary>
        /// Amount of money spent today (in kroner)
        /// </summary>
        public int SpentToday { get; set; } = 0;

        /// <summary>
        /// Amount of money spent this month (in kroner)
        /// </summary>
        public int SpentThisMonth { get; set; } = 0;


        public Account(int balance)
        {
            AccountNumber = RegistrationNumber + GenerateAccountNumber();
            Balance = balance;
        }

        /// <summary>
        /// Generates a unique account number for each customer
        /// </summary>
        /// <returns></returns>
        public string GenerateAccountNumber()
        {
            // Used to generate digits for account number
            Random r = new Random();

            // Stores the account number
            string accNumber = "";

            // Loops 10 times to get 10 unique digits
            for (int i = 0; i < 10; i++)
            {
                int nextDigit = r.Next(0, 10);
                accNumber += nextDigit.ToString(); // Concatenate the latest digit to the full account number
            }

            // Return accNumber as an integer
            return accNumber;
        }

        public void ResetDailyLimit()
        {
            this.SpentToday = 0;
        }

        public void ResetMonthlyLimit()
        {
            this.SpentThisMonth = 0;
        }

        /// <summary>
        /// Add more money to the account.
        /// </summary>
        /// <param name="amount">Amount of money to add to the account.</param>
        public void Deposit(int amount)
        {
            this.Balance += amount;
        }

        public override string ToString()
        {
            return "Balance: " + Balance + "\n" + "Spent today: " + SpentToday + "\n" + "Spent this month: " + SpentThisMonth;
        }
    }
}
