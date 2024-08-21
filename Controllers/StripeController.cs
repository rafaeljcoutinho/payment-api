using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using API.Models;

[ApiController]
[Route("api/[controller]")]
public class StripeController : ControllerBase
{
    private readonly string _stripeSecretKey;

    public StripeController(IConfiguration configuration)
    {
        _stripeSecretKey = configuration["Stripe:SecretKey"];
        StripeConfiguration.ApiKey = _stripeSecretKey;
    }

    [HttpPost("create-checkout-session")]
    public async Task<IActionResult> CreateCheckoutSession([FromBody] CreateCheckoutSessionRequest req)
    {
        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string>
            {
                "card",
            },
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    Price = req.PriceId ,
                    Quantity = 1,
                },
            },
            Mode = "subscription",
            SuccessUrl = "https://yourdomain.com/success",
            CancelUrl = "https://yourdomain.com/cancel",
        };

        var service = new SessionService();
        Session session = service.Create(options);

        return Ok(new { id = session.Id });
    }
}