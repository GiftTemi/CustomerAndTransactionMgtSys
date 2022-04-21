using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAndTransactionMgt.Models.Enums
{
    public enum TransactionChannels
    {
        [Description("ussd")]
        USSD = 1,
        [Description("Moile Banking")]
        MOBILEBANKING = 2,
        [Description("Internet Banking")]
        INTERNETBANKING = 3
    }
}
