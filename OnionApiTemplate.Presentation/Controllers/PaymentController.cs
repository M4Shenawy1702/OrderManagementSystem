using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Application.IServices;

[ApiController]
[Route("api/payments")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpGet("success")]
    public async Task<IActionResult> CheckoutSuccessAsync(string sessionId, int orderId)
    {
        try
        {
            var result = await _paymentService.CheckoutSuccess(sessionId, orderId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("failed")]
    public async Task<IActionResult> CheckoutFailed(string sessionId)
    {
        return Ok(await _paymentService.CheckoutFailed(sessionId));
    }
}
