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
        [HttpGet("GetAll")] //API untuk mendapatkan semua data Invoice yang sudah disubmit
        public IActionResult GetAllInvoice()
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

        [HttpGet("GetAllProps")] //API untuk mendapatkan semua properti yang dibutuhkan UI
        public IActionResult GetAllProps()
        {
            var response = new GenericResponseModel<InvoicePropertyModel>();
            var bl = new InvoiceBL();

            response = bl.GetAllProps();

            if (!response.Status)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("InsertInvoice")] //API untuk menambahkan invoice baru
        public IActionResult InsertInvoice([FromBody] InvoiceModel data)
        {
            var response = new GenericResponseModel<bool>();
            var bl = new InvoiceBL();

            response = bl.InsertInvoice(data);

            if (!response.Status)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("GetNewInvoice")] //API untuk mendapatkan data invoice terbaru
        public IActionResult GetNewInvoice()
        {
            var response = new GenericResponseModel<InvoiceModel>();
            var bl = new InvoiceBL();

            response = bl.GetInvoiceDetail();

            if (!response.Status)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
