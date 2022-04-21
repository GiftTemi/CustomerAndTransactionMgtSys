using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerAndTransactionMgt.Models.ViewModel
{
    public class UpdateCustomerVM
    {
        [Required(ErrorMessage = "Please Supply First Name")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Please Supply Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please Enter Date Of Birth")]
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please Enter Phone Number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Please Enter Gender")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Please Enter BVN")]
        public string BVN { get; set; }
    }
}
