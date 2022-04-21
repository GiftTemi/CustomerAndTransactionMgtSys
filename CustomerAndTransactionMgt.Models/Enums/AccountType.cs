using System.ComponentModel;

namespace CustomerAndTransactionMgt.Models.Enums
{
    public enum AccountType
    {
        [Description("Unknown")]
        UNKNOWN = 0,
        [Description("Savings")]
        SAVINGS = 1,
        [Description("Current")]
        CURRENT = 2
    }
}
