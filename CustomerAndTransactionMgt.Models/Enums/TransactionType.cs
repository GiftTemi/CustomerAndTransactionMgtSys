using System.ComponentModel;

namespace CustomerAndTransactionMgt.Models.Enums
{
    public enum TransactionType
    {
        
        [Description("Debit")]
        DEBIT = 1,
        [Description("Credit")]
        CREDIT = 2,
    }
}
