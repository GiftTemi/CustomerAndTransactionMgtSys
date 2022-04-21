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
    public class CustomerController : ControllerBase
    {
        private IMediator mediator;
        public CustomerController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // URL - https://localhost:44378/api/Customer type POST
        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerCommand command)
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
            catch (Exception)
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

        // URL - https://localhost:44378/api/Customer type GET
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await mediator.Send(new GetAllCustomersQuery());
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
        [HttpGet("{bvn}")]
        public async Task<IActionResult> GetCustomerByBVN(string bvn)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await mediator.Send(new GetCustomerByBVNQuery { BVN = bvn });
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

        //// URL - https://localhost:44378/api/Customer/{bvn} type PUT
        [HttpPut("{bvn}")]
        public async Task<IActionResult> Update(string bvn, UpdateCustomerCommand command)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    command.BVN = bvn;
                    var response = await mediator.Send(command);
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

                return BadRequest(new ResponseModel()
                {
                    Data = "",
                    Message = "Customer details not Modified",
                    StatusCode = HttpStatusCode.BadRequest

                });
            }
        }
        
        [HttpPatch("{bvn}")]
        public async Task<IActionResult> Disable(string bvn, DisableCustomerCommand command)
        {

            if (ModelState.IsValid)
            {
                command.BVN = bvn;
                var response = await mediator.Send(command);
                return Ok(response);
            }
            else
            {
                return BadRequest(new ResponseModel()
                {
                    Data = "",
                    Message = "Customer details not Modified",
                    StatusCode = HttpStatusCode.BadRequest

                });
            }
        }
    }
}
