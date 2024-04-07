using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stripe;
using System.IO;
using System.Threading.Tasks;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.Core.Entites.Order_Aggregate;
using Talabat.Core.Services;

namespace Talabat.APIs.Controllers
{

    public class PaymentsController : BaseAPIsController
    {
        private readonly IPaymentServices _paymentServices;
        private readonly ILogger<PaymentsController> _logger;

        public PaymentsController(
            IPaymentServices paymentServices,
            ILogger<PaymentsController> logger)
        {
            _paymentServices = paymentServices;
            _logger = logger;
        }

        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string basketId)
        {

            var basket = await _paymentServices.CreateOrUpdatePaymentinten(basketId);

            if (basket == null) return BadRequest(new ApiResponese(400, "A Propelm with basket "));

            return Ok(basket);
        }

        //[HttpPost("webhook")]
        //public async Task<ActionResult> stripeWebhook()
        //{
        //    var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        //    try
        //    {
        //        var stripeEvent = EventUtility.ConstructEvent(json,
        //            Request.Headers["Stripe-Signature"], "");

        //        PaymentIntent intent;
        //        Order order;

                //// Handle the event
                //switch (stripeEvent.Type)
                //{
                //    case Events.PaymentIntentSucceeded:
                //        intent = (PaymentIntent)stripeEvent.Data.Object;
                //        order = await _paymentServices.UpdatePaymentIntentSucceededOrFailed(intent.Id, true);
                //        _logger.LogInformation("Payment Succeeded");

                //        break;
                //    case Events.PaymentIntentPaymentFailed:
                //        intent = (PaymentIntent)stripeEvent.Data.Object;
                //        order = await _paymentServices.UpdatePaymentIntentSucceededOrFailed(intent.Id, true);
                //        _logger.LogInformation("Payment Failed");


                //        break;
                //    default:
                //        break;
                //}
            
    
            
    
       }
    }
