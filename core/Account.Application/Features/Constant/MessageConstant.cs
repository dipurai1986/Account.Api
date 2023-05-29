using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Application.Features.Constant
{
    public static  class MessageConstant
    {
        public static string MAX_AMOUNT_PER_TRANSACTION_LIMIT_MESSAGGE = "Invalid Amount it Must not b More Than ";
        public static string MAX_AMOUNT_PER_TRANSACTION_BALANCE_PERCENT_MESSAGE = "You can not withdraw more than 90% of your account in one transaction";
        public static string BALANCE_LIMIT_MESSAGE = "Insufficient balance for withdrawal";
        public static string INVALID_AMOUNT_MESSAGE = "Invalid Amount";
        public static string AMOUNT_REQUIRE_MESSAGE = "Amount must be required";
        public static string ACCOUNT_REQUIRE_MESSAGE = "Account Id must be required";
   
    }
}
