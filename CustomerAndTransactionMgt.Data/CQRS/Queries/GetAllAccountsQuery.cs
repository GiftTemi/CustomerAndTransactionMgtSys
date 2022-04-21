using CustomerAndTransactionMgt.Models.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerAndTransactionMgt.Data.CQRS.Queries
{
    public class GetAllAccountsQuery : IRequest<IEnumerable<Account>>
    {
        public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, IEnumerable<Account>>
        {
            private CustomerAndTransactionContext context;
            public GetAllAccountsQueryHandler(CustomerAndTransactionContext context)
            {
                this.context = context;
            }
            public async Task<IEnumerable<Account>> Handle(GetAllAccountsQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    var accountList = await context.Accounts.ToListAsync();
                    return accountList;
                }
                catch (Exception ex)
                {//Log errors here
                    return default(IEnumerable<Account>);
                }
            }
        }
    }
}

