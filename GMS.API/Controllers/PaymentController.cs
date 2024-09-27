using GMS.Service.Dtos;
using GMS.Service.Dtos.Payment;
using GMS.Service.Services.Payments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost]
        public async Task<IActionResult> CreatePaymentAsync([FromBody] PaymentCreateDto paymentCreateDto)
        {
            var response = await _paymentService.CreatePaymentAsync(paymentCreateDto);
            return ReturnFormattedResponse(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePaymentAsync(Guid id, [FromBody] PaymentCreateDto paymentCreateDto)
        {
            var response = await _paymentService.UpdatePaymentAsync(id, paymentCreateDto);
            return ReturnFormattedResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentAsync(Guid id)
        {
            var response = await _paymentService.DeletePaymentAsync(id);
            return ReturnFormattedResponse(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentByIdAsync(Guid id)
        {
            var response = await _paymentService.GetPaymentByIdAsync(id);
            return ReturnFormattedResponse(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaymentsAsync([FromQuery] PaymentParametersDto queryParameters)
        {
            var response = await _paymentService.GetAllPaymentAsync(queryParameters);
            return ReturnFormattedResponse(response);
        }
    }
}
