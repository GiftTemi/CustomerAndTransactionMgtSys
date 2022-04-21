using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using CustomerAndTransactionMgt.Models.Model;
using System.ComponentModel.DataAnnotations;

namespace CustomerAndTransactionMgt.Data.CQRS.Commands
{
    public class DisableCustomerCommand : IRequest<ResponseModel>
    {
        [Required(ErrorMessage = "Please Enter BVN")]
        [StringLength(11)]
        public string BVN { get;set; }

        public class DisableCustomerCommandHandler : IRequestHandler<DisableCustomerCommand, ResponseModel>
        {
            private CustomerAndTransactionContext context;
            public DisableCustomerCommandHandler(CustomerAndTransactionContext context)
            {
                this.context = context;
            }
            public async Task<ResponseModel> Handle(DisableCustomerCommand command, CancellationToken cancellationToken)
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
                        customer.IsDisabled = true;

                        await context.SaveChangesAsync();
                        return new ResponseModel("Customer Disabled Successfully", customer, HttpStatusCode.OK);
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


