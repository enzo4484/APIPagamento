using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Pagamento.Domain.Pedido;

namespace APIPagamento.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly Pagemento.Application.Services.IPaymentService _paymentService;

        public PaymentsController(Pagemento.Application.Services.IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // Create: calls Pagar.me and stores the pedido locally
        [HttpPost(Name = "CreatePayment")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CreatePayment([FromBody] Pedido pedido)
        {
            if (pedido == null)
                return BadRequest("Pedido is required");

            if (pedido.Id == Guid.Empty)
                pedido.Id = Guid.NewGuid();

            pedido.CreatedAt = DateTime.UtcNow;
            pedido.UpdatedAt = pedido.CreatedAt;

            var result = await _paymentService.CreateAsync(pedido);
            return Content(result.Raw, "application/json");
        }

        // Read all
        [HttpGet(Name = "GetAllPayments")]
        public IActionResult GetAll()
        {
            var list = _paymentService.GetAll();
            return Ok(list);
        }

        // Read by id
        [HttpGet("{id}", Name = "GetPayment")]
        public IActionResult Get(Guid id)
        {
            var pedido = _paymentService.Get(id);
            if (pedido == null) return NotFound();
            return Ok(pedido);
        }

        // Update
        [HttpPut("{id}", Name = "UpdatePayment")]
        public IActionResult Update(Guid id, [FromBody] Pedido pedido)
        {
            if (pedido == null)
                return BadRequest("Pedido is required");

            if (_paymentService.Get(id) == null)
                return NotFound();

            _paymentService.Update(id, pedido);
            return NoContent();
        }

        // Delete (remove)
        [HttpDelete("{id}", Name = "DeletePayment")]
        public IActionResult Delete(Guid id)
        {
            if (_paymentService.Delete(id))
                return NoContent();

            return NotFound();
        }
    }
}
