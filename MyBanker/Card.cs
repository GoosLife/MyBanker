using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBanker
{
    public abstract class Card
    {
        /// <summary>
        /// The customer that is applying for the card
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// How old a customer has to be to obtain a certain card
        /// </summary>
        protected int RequiredAge { get; set; }

        /// <summary>
        /// The prefixes associated with this type of cards' card number.
        /// </summary>
        protected List<int> Prefixes { get; set; }

        /// <summary>
        /// The length of the card number associated with this type of card
        /// </summary>
        protected int CardNumberLength { get; set; } = 16;

        /// <summary>
        /// A 10 digit number that, alongside the registration number, makes up the customers full account number
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// A unique string of digits associated with one card.
        /// The number of characters in this string is specified in CardNumberLength.
        /// </summary>
        public string CardNumber { get; set; }

        protected Card(Customer customer)
        {
            Customer = customer;
            AccountNumber = Customer.Account.AccountNumber;
        }

        /// <summary>
        /// Generates a unique cardnumber for each card
        /// </summary>
        /// <returns></returns>
        protected string GenerateCardNumber()
        {
            // Stores the card number
            string cardNumber = "";

            // Random number generator used to determine prefix and digits in card number.
            Random r = new Random();

            // Choose a prefix to use
            string prefix = Prefixes[r.Next(0, Prefixes.Count)].ToString();
            cardNumber += prefix + " "; // Adds the prefix to the beginning of the card number

            // Determine how many digits will need to be generated
            int digitsToGenerate = this.CardNumberLength - prefix.Length;

            // Generate card number
            for (int i = 0; i < digitsToGenerate; i++)
            {
                int nextDigit = r.Next(0,10);
                cardNumber += nextDigit.ToString(); // Concatenate latest digit to cardnumber.
            }

            return cardNumber;
        }

        public bool CheckRequirements()
        {
            // If customer lives up to the requirements for this card, it can be generated
            if (this.Customer.Age >= RequiredAge)
            {
                return true;
            }

            // Otherwise, the operation fails
            return false;
        }

        /// <summary>
        /// Simulates a domestic transaction.
        /// </summary>
        /// <param name="amount">Monetary value of transaction</param>
        /// <returns></returns>
        public virtual string DomesticTransaction(int amount)
        {
            if (amount <= Customer.Account.Balance)
            {
                Customer.Account.Balance -= amount;
                return $"Du har brugt {amount} kroner.";
            }
            else
            {
                return $"Du har ikke nok penge på kontoen.\nTilgængelig saldo: { Customer.Account.Balance }";
            }
        }
    }
}
