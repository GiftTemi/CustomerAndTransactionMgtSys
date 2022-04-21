using CustomerAndTransactionMgt.Models.Enums;
using System;

namespace CustomerAndTransactionMgt.Models.ViewModel
{
    public class CreateTransactionVM
    {
        public decimal Amount { get; set; }
        public string Narration { get; set; }
        public TransactionType TransactionType { get; set; }
        public string FromAcct { get; set; }
        public string ToAcct { get; set; }
        public TransactionChannels TransactionChannels { get; set; }
        public int AccountId { get; set; }
    }
}
