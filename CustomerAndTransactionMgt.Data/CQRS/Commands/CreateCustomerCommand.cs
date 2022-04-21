using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using CustomerAndTransactionMgt.Models.Model;

namespace CustomerAndTransactionMgt.Data.CQRS.Commands
{
    public class CreateCustomerCommand : IRequest<ResponseModel>
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

        public class CreateProductCommandHandler : IRequestHandler<CreateCustomerCommand, ResponseModel>
        {
            private CustomerAndTransactionContext context;
            public CreateProductCommandHandler(CustomerAndTransactionContext context)
            {
                this.context = context;
            }
            public async Task<ResponseModel> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
            {
                ResponseModel responseModel = null;
                try
                {
                    var customer = context.Customers.Where(a => a.BVN == command.BVN).FirstOrDefault();

                    if (customer != null)
                    {
                        responseModel = new ResponseModel
                        {
                            Data = customer,
                            Message = "BVN Already exists",
                            StatusCode = HttpStatusCode.Forbidden
                        };
                    }
                    else
                    {
                        var isPhone = context.Customers.Where(c => c.PhoneNumber == command.PhoneNumber).FirstOrDefault();
                        if (isPhone == null)
                        {
                            var newCustomer = new Customer
                            {
                                Address = command.Address,
                                LGA = command.LGA,
                                BVN = command.BVN,
                                CreatedDate = DateTime.Now,
                                DOB = command.DOB,
                                Email = command.Email,
                                FirstName = command.FirstName,
                                Gender = command.Gender,
                                LastName = command.LastName,
                                MiddleName = string.IsNullOrEmpty(command.MiddleName)?default:command.MiddleName,
                                PhoneNumber = command.PhoneNumber,
                                MotherMaidenName = command.MotherMaidenName,
                                ModifiedDate = DateTime.Now,
                                State = command.State,
                                CustomerNumber = (new Random()).Next(0, 100000).ToString("D5"),
                                IsDisabled = false,

                            };
                            context.Customers.Add(newCustomer);
                            await context.SaveChangesAsync();
                            responseModel = new ResponseModel
                            {
                                Data = newCustomer,
                                Message = "Customer Created Successfully",
                                StatusCode = HttpStatusCode.OK
                            };
                        }
                        else
                        {
                            responseModel = new ResponseModel
                            {
                                Data = isPhone,
                                Message = "Customer Not Created. Use another Phone number and try again",
                                StatusCode = HttpStatusCode.Forbidden
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    //log exception Here
                    responseModel= new ResponseModel
                    {
                        Data = null,
                        Message = "An error occured",
                        StatusCode = HttpStatusCode.BadGateway
                    };
                }
                return responseModel;
            }
        }
    }
}
