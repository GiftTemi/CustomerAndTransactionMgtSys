using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAndTransactionMgt.Models.Model
{
    public class Customer : BaseModel
    {
        [Required(ErrorMessage = "Please Supply First Name")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Please Supply Customer Number")]
        [StringLength(5)]
        public string CustomerNumber { get; set; }
        [Required(ErrorMessage = "Please Supply Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please Enter Date Of Birth")]
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please Enter Phone Number")]
        [StringLength(11)]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Please Enter Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Please Enter BVN")]
        [StringLength(11)]
        public string BVN { get; set; }
        [Required(ErrorMessage = "Please Enter State Of Origin")]
        public string State { get; set; }
        [Required(ErrorMessage = "Please Enter Local Govt. Area")]
        public string LGA { get; set; }

        [Required(ErrorMessage = "Please Enter Mother's Maiden Name")]
        public string MotherMaidenName { get; set; }
    }
}
