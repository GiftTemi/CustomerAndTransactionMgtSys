using System;

namespace CustomerAndTransactionMgt.Models.Model
{
    public class BaseModel
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDisabled { get; set; }
    }
}
