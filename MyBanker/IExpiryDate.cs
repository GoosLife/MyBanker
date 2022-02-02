using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBanker
{
    public interface IExpiryDate
    {
        /// <summary>
        /// The MM/yy formatted date upon which the card expires
        /// </summary>
        string ExpiryDate { get; set; }

        public string CalculateExpiryDate(int years, int months);
    }
}
