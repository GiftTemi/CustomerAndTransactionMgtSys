using CustomerAndTransactionMgt.Models.Enums;
using System;

namespace CustomerAndTransactionMgt.Models.ViewModel
{
    public class CreateAccountVM
    {
        public string Nuban { get; set; }
        public string CustomerNumber { get; set; }
        public string LegerCode { get; set; }
        public string BranchCode { get; set; }
        public Guid CustomerId { get; set; }
        public AccountType AccountType { get; set; }
    }
}
