using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using CustomerAndTransactionMgt.Models.Model;
using CustomerAndTransactionMgt.Models.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace CustomerAndTransactionMgt.Data.CQRS.Commands
{
    public class UpdateCustomerCommand : IRequest<ResponseModel>
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

        public string BVN { get; set; }

        public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, ResponseModel>
        {
            private CustomerAndTransactionContext context;
            public UpdateCustomerCommandHandler(CustomerAndTransactionContext context)
            {
                this.context = context;
            }
            public async Task<ResponseModel> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var customer = context.Customers.Where(a => a.BVN == command.BVN).FirstOrDefault();

                    if (customer == null)
                    {
                        return new ResponseModel
                        {
                            Data = "",
                            Message = "Customer Not found",
                            StatusCode = HttpStatusCode.NotFound
                        };
                    }
                    else
                    {
                        customer.Address = command.Address;
                        
                        customer.CreatedDate = DateTime.Now;
                        customer.DOB = command.DOB;
                        customer.Email = command.Email;
                        customer.FirstName = command.FirstName;
                        customer.Gender = command.Gender;
                        customer.LastName = command.LastName;
                        customer.MiddleName = command.MiddleName;
                        customer.PhoneNumber = command.PhoneNumber;
                        customer.ModifiedDate = DateTime.Now;
                        

                        await context.SaveChangesAsync();
                        return new ResponseModel("Customer Details Updated Successfully", customer, HttpStatusCode.OK);
                    }
                }
                catch (Exception ex)
                {
                    return new ResponseModel
                    {
                        Data = "",
                        Message = "An error occured",
                        StatusCode = HttpStatusCode.BadGateway
                    };
                }
            }
        }
    }
}

