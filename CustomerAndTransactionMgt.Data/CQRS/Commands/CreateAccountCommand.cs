using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using CustomerAndTransactionMgt.Models.Model;
using CustomerAndTransactionMgt.Models.Enums;

namespace CustomerAndTransactionMgt.Data.CQRS.Commands
{
    public class CreateAccountCommand : IRequest<ResponseModel>
    {
        [Required(ErrorMessage = "Customer Number Required")]
        [StringLength(5)]
        public string CustomerNumber { get; set; }

        [Required(ErrorMessage = "Ledger Code Required")]
        public LedgerCodes legerCode { get; set; }

        [Required(ErrorMessage = "Branch Code Required")]
        [StringLength(3)]
        public int BranchCode { get; set; }
        public class CreateProductCommandHandler : IRequestHandler<CreateAccountCommand, ResponseModel>
        {
            private CustomerAndTransactionContext context;
            public CreateProductCommandHandler(CustomerAndTransactionContext context)
            {
                this.context = context;
            }
            public async Task<ResponseModel> Handle(CreateAccountCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var customer = context.Customers.Where(c => c.CustomerNumber == command.CustomerNumber).FirstOrDefault();
                    if (customer != null)
                    {
                        var account = context.Accounts.Where(a => a.CustomerNumber == command.CustomerNumber)
                            .Where(a => a.legerCode == command.legerCode).FirstOrDefault();

                        if (account != null)
                        {
                            return new ResponseModel
                            {
                                Data = account,
                                Message = "Account Already exists",
                                StatusCode = HttpStatusCode.Found
                            };
                        }
                        else
                        {
                            var newAccount = new Account
                            {
                                CreatedDate = DateTime.Now,
                                Nuban = "00" + (new Random()).Next(0, 100000000).ToString("D8").ToString(),
                                CustomerNumber = command.CustomerNumber,
                                legerCode = command.legerCode,
                                BranchCode = command.BranchCode,
                                Customer = customer,
                                CustomerId = customer.Id,
                                IsPND = false,
                            };
                            context.Accounts.Add(newAccount);
                            await context.SaveChangesAsync();
                            return new ResponseModel
                            {
                                Data = newAccount,
                                Message = "Account Created Successfully",
                                StatusCode = HttpStatusCode.OK
                            };
                        }
                    }
                    else
                    {
                        return new ResponseModel
                        {
                            Data = "",
                            Message = "Customer number not found",
                            StatusCode = HttpStatusCode.OK
                        };
                    }
                }
                catch (Exception ex)
                {
                    //log exception Here
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

