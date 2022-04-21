using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Net;
using CustomerAndTransactionMgt.Data.CQRS.Commands;
using CustomerAndTransactionMgt.Data.CQRS.Queries;
using CustomerAndTransactionMgt.Models.Model;

namespace CustomerAndTransactionMgtSys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private IMediator mediator;
        public TransactionController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // URL - https://localhost:44378/api/Customer type GET
        [HttpGet]
        public async Task<IActionResult> GetAllTransactions()
        {
            try
            {
                var response = await mediator.Send(new GetAllTransactionsQuery());
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel()
                {
                    Data = "",
                    Message = "An error occured.",
                    StatusCode = HttpStatusCode.BadRequest

                });
            }
        }

        // URL - https://localhost:44378/api/Customer/{bvn} type GET
        [HttpGet("{Nuban}")]
        public async Task<IActionResult> GetTransactionByNuban(string Nuban)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await mediator.Send(new GetTransactionsByNubanQuery { NUBAN = Nuban });
                    return Ok(response);
                }
                else
                {
                    return BadRequest(new ResponseModel()
                    {
                        Data = "",
                        Message = "Data supplied is not valid.",
                        StatusCode = HttpStatusCode.BadRequest

                    });
                }
            }
            catch (Exception ex)
            {
                //log exception here return BadRequest(new ResponseModel()
                return BadRequest(new ResponseModel()
                {
                    Data = "",
                    Message = "An error occured.",
                    StatusCode = HttpStatusCode.BadRequest

                });
            }
        }

        // URL - https://localhost:44378/api/Customer type POST
        [HttpPost]
        public async Task<IActionResult> PostTransaction (CreateTransactionCommand command)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var response = await mediator.Send(command);
                    return Ok(response);
                }
                else
                {
                    return BadRequest(new ResponseModel()
                    {
                        Data = "",
                        Message = "There were inconsistencies in your request",
                        StatusCode = HttpStatusCode.BadRequest

                    });
                }
            }
            catch (Exception ex)
            {
                //log exception here
                return BadRequest(new ResponseModel()
                {
                    Data = "",
                    Message = "An error occured. Customer not created",
                    StatusCode = HttpStatusCode.BadRequest

                });
            }
        }

    }
}

