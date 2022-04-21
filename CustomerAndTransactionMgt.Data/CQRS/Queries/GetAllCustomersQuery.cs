using CustomerAndTransactionMgt.Models.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerAndTransactionMgt.Data.CQRS.Queries
{
    public class GetAllCustomersQuery : IRequest<IEnumerable<Customer>>
    {
        public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<Customer>>
        {
            private CustomerAndTransactionContext context;
            public GetAllCustomersQueryHandler(CustomerAndTransactionContext context)
            {
                this.context = context;
            }
            public async Task<IEnumerable<Customer>> Handle(GetAllCustomersQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    var customerList = await context.Customers.ToListAsync();
                    return customerList;
                }
                catch (Exception ex)
                {//Log errors here
                    return default(IEnumerable<Customer>);
                }
            }
        }
    }
}
