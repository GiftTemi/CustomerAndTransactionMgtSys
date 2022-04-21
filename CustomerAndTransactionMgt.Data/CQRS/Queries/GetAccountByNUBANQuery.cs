using CustomerAndTransactionMgt.Models.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerAndTransactionMgt.Data.CQRS.Queries
{
    public class GetAccountByNUBANQuery : IRequest<Account>
    {
        [Required(ErrorMessage = "Please Supply Nuban")]
        [StringLength(10)]
        public string NUBAN { get; set; }
        public class GetAccountByBVNQueryHandler : IRequestHandler<GetAccountByNUBANQuery, Account>
        {
            private CustomerAndTransactionContext context;
            public GetAccountByBVNQueryHandler(CustomerAndTransactionContext context)
            {
                this.context = context;
            }
            public async Task<Account> Handle(GetAccountByNUBANQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    var account = await context.Accounts.Where(a => a.Nuban == query.NUBAN).FirstOrDefaultAsync();
                    try
                    {
                        var customer = await context.Customers.Where(a => a.CustomerNumber == account.CustomerNumber).FirstOrDefaultAsync();
                        account.Customer = customer != null ? customer : default;

                    }
                    catch (Exception ex)
                    {
                        //Log error here
                    }
                    return account;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}

