using CustomerAndTransactionMgt.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace CustomerAndTransactionMgt.Models.Model
{
    public class Transaction : BaseModel
    {

        [Required(ErrorMessage = "Please Supply Amount")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
        public string Narration { get; set; }
        [Required(ErrorMessage = "Suppply Transaction Type")]
        public TransactionType TransactionType { get; set; }
        [Required(ErrorMessage = "Please Supply Nuban to debit")]
        [StringLength(10)]
        public string FromAcct { get; set; }
        [Required(ErrorMessage = "Please Supply Nuban to credit")]
        [StringLength(10)]
        public string ToAcct { get; set; }
        [Required]
        public TransactionChannels TransactionChannels { get; set; }

        public Account Account { get; set; }
        public int AccountId { get; set; }
    }
}
