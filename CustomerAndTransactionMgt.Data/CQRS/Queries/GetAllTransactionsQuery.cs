using CustomerAndTransactionMgt.Models.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerAndTransactionMgt.Data.CQRS.Queries
{
    public class GetAllTransactionsQuery : IRequest<IEnumerable<Transaction>>
    {
        public class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQuery, IEnumerable<Transaction>>
        {
            private CustomerAndTransactionContext context;
            public GetAllTransactionsQueryHandler(CustomerAndTransactionContext context)
            {
                this.context = context;
            }
            public async Task<IEnumerable<Transaction>> Handle(GetAllTransactionsQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    var customerList = await context.Transactions.ToListAsync();
                    return customerList;
                }
                catch (Exception ex)
                {//Log errors here
                    return default(IEnumerable<Transaction>);
                }
            }
        }
    }
}

