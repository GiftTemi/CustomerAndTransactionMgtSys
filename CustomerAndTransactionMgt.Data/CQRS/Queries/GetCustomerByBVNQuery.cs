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
    public class GetCustomerByBVNQuery : IRequest<Customer>
    {
        [Required(ErrorMessage = "Please Enter BVN")]
        [StringLength(11)]
        public string BVN { get; set; }
        public class GetCustomerByBVNQueryHandler : IRequestHandler<GetCustomerByBVNQuery, Customer>
        {
            private CustomerAndTransactionContext context;
            public GetCustomerByBVNQueryHandler(CustomerAndTransactionContext context)
            {
                this.context = context;
            }
            public async Task<Customer> Handle(GetCustomerByBVNQuery query, CancellationToken cancellationToken)
            {
                try
                {

                    var customer = await context.Customers.Where(a => a.BVN == query.BVN).FirstOrDefaultAsync();
                    return customer;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}
