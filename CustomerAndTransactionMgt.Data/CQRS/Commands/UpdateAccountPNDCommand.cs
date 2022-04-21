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
    public class UpdateAccountPNDCommand : IRequest<ResponseModel>
    {
        [Required(ErrorMessage = "Please Supply Nuban")]
        public string Nuban { get; set; }
        [Required(ErrorMessage = "Please Supply IsPND")]
        public bool IsPND  { get; set; }
        public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountPNDCommand, ResponseModel>
        {
            private CustomerAndTransactionContext context;
            public UpdateAccountCommandHandler(CustomerAndTransactionContext context)
            {
                this.context = context;
            }
            public async Task<ResponseModel> Handle(UpdateAccountPNDCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var account = context.Accounts.Where(a => a.Nuban == command.Nuban).FirstOrDefault();
                    if (account == null)
                    {
                        return new ResponseModel
                        {
                            Data = "",
                            Message = "Account Not found",
                            StatusCode = HttpStatusCode.NotFound
                        };
                    }
                    else
                    {
                        string insert = command.IsPND ? "placed on" : "removed from";
                        if (account.IsPND==command.IsPND)
                        {
                            return new ResponseModel
                            {
                                Data = account,
                                Message = $"Account already {insert} PND",
                                StatusCode = HttpStatusCode.NotFound

                            };
                        }
                        account.IsPND = command.IsPND;                        
                        await context.SaveChangesAsync();
                        
                        return new ResponseModel
                        {
                            Data = account,
                            Message = $"Account succeccfully {insert} PND",
                            StatusCode = HttpStatusCode.OK
                        };
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

