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
    public class GetTransactionsByNubanQuery : IRequest<Transaction>
    {
        [Required(ErrorMessage = "Please Supply Nuban")]
        [StringLength(10)]
        public string NUBAN { get; set; }
        public class GetAccountByBVNQueryHandler : IRequestHandler<GetTransactionsByNubanQuery, Transaction>
        {
            private CustomerAndTransactionContext context;
            public GetAccountByBVNQueryHandler(CustomerAndTransactionContext context)
            {
                this.context = context;
            }
            public async Task<Transaction> Handle(GetTransactionsByNubanQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    var transaction = await context.Transactions.Where(t => t.FromAcct == query.NUBAN).FirstOrDefaultAsync();
                    return transaction;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}

