using Invoice.API.Logic;
using Invoice.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        [HttpGet("GetAll")]
        public IActionResult TestApi()
        {
            var response = new GenericResponseModel<List<InvoiceModel>>();
            var bl = new InvoiceBL();

            response = bl.GetAll();

            if (!response.Status)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
