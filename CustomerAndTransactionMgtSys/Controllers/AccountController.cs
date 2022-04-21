using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Net;
using CustomerAndTransactionMgt.Models.Model;
using CustomerAndTransactionMgt.Data.CQRS.Commands;
using CustomerAndTransactionMgt.Data.CQRS.Queries;

namespace AccountAndTransactionMgtSys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IMediator mediator;
        public AccountController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // URL - https://localhost:44378/api/Account type POST
        [HttpPost]
        public async Task<IActionResult> CreateAccount(CreateAccountCommand command)
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
                    Message = "An error occured. Account not created",
                    StatusCode = HttpStatusCode.BadRequest

                });
            }
        }

        // URL - https://localhost:44378/api/Account type GET
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await mediator.Send(new GetAllAccountsQuery());
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

        // URL - https://localhost:44378/api/Account/{Nuban} type GET
        [HttpGet("{Nuban}")]
        public async Task<IActionResult> GetAccountByNUBAN(string Nuban)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await mediator.Send(new GetAccountByNUBANQuery { NUBAN = Nuban });
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


        [HttpPatch()]
        public async Task<IActionResult> PlaceOnPND(UpdateAccountPNDCommand command)
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
                    Message = "Account details not Modified",
                    StatusCode = HttpStatusCode.BadRequest

                });
            }
        }
    }
}

