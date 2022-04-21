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
    public class CreateTransactionCommand : IRequest<ResponseModel>
    {

        [Required(ErrorMessage = "Please Supply Amount")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
        public string Narration { get; set; }
        [Required(ErrorMessage ="Suppply Transaction Type")]
        public TransactionType TransactionType { get; set; }
        [Required(ErrorMessage = "Please Supply Nuban to debit")]
        [StringLength(10)]
        public string FromAcct { get; set; }
        [Required(ErrorMessage = "Please Supply Nuban to credit")]
        [StringLength(10)]
        public string ToAcct { get; set; }
        [Required]
        public TransactionChannels TransactionChannels { get; set; }

        public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, ResponseModel>
        {
            private CustomerAndTransactionContext context;
            public CreateTransactionCommandHandler(CustomerAndTransactionContext context)
            {
                this.context = context;
            }
            public async Task<ResponseModel> Handle(CreateTransactionCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var accountFrom = context.Accounts.Where(t => t.Nuban == command.FromAcct).FirstOrDefault();
                    if (accountFrom != null)
                    {
                        var accountTo = context.Accounts.Where(t => t.Nuban == command.ToAcct).FirstOrDefault();
                        if (accountTo != null)
                        {
                            Transaction transction = default;
                            try
                            {
                                transction = context.Transactions.Where(t => t.CreatedDate == DateTime.Now)
                                                        .Where(t => t.Amount == command.Amount)
                                                        .Where(t => t.ToAcct == command.ToAcct).FirstOrDefault();
                            }
                            catch (Exception ex)
                            {
                            }
                            if (transction != null)
                            {
                                return new ResponseModel
                                {
                                    Data = transction,
                                    Message = "No duplicate transaction allowed",
                                    StatusCode = HttpStatusCode.Found
                                };
                            }
                            else
                            {
                                var newTransaction = new Transaction
                                {
                                    Amount = command.Amount,
                                    FromAcct = command.FromAcct,
                                    ToAcct = command.ToAcct,
                                    CreatedDate = DateTime.Now,
                                    AccountId = accountFrom.Id,
                                    IsDisabled = false,
                                    Narration = string.IsNullOrEmpty(command.Narration) ? default : command.Narration
                                };
                                context.Transactions.Add(newTransaction);
                                await context.SaveChangesAsync();
                                return new ResponseModel
                                {
                                    Data = newTransaction,
                                    Message = "Transaction Posted Successfully",
                                    StatusCode = HttpStatusCode.OK
                                };
                            }
                        }
                        else
                        {
                            return new ResponseModel
                            {
                                Data = "",
                                Message = "Account to be credited does not exist",
                                StatusCode = HttpStatusCode.OK
                            };
                        }
                    }
                    else
                    {
                        return new ResponseModel
                        {
                            Data = "",
                            Message = "Account to be debited does not exist",
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
