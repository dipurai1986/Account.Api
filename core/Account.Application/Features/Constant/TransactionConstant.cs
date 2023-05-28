using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Application.Features.Constant
{
    public static class TransactionConstant
    {
        public static decimal INVALID_MAX_TRANSACTION_AMOUNT = 10000;
        public static decimal MIN_BALANCE = 100;

        public static decimal MAX_WITHDRAW_LIMIT = 0.9m;
    }
}
