using CustomerAndTransactionMgt.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerAndTransactionMgt.Models.Model
{
    public class Account : BaseModel
    {
        [Required]
        public string AccountType { get { return legerCode.ToString(); } }

        [Required(ErrorMessage = "Please Supply Nuban")]
        [StringLength(10)]
        public string Nuban { get; set; }
        [Required(ErrorMessage = "Please Supply CustomerNumber")]
        [StringLength(10)]
        public string CustomerNumber { get; set; }

        [Required(ErrorMessage = "Please Supply Ledger Code")]
        [StringLength(3)]
        public LedgerCodes legerCode { get; set; }
        [Required(ErrorMessage = "Please Supply Branch Code")]
        [StringLength(3)]
        public int BranchCode { get; set; }
        [Required(ErrorMessage = "Please Supply Branch Code")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        [Required(ErrorMessage = "PND Status is Required")]
        public bool IsPND { get; set; }
    }
}