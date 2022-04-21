using System.ComponentModel;

namespace CustomerAndTransactionMgt.Models.Enums
{
    public enum LedgerCodes
    {
        [Description("Savings")]
        SAVINGS = 101,
        [Description("Current")]
        CURRENT = 103,
        [Description("Domiciliary")]
        DOMICILIARY = 106
    }
}

