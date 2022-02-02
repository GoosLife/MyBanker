using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBanker
{
    public interface ICreditCard
    {
        int CreditLimit { get; set; }
    }
}
