using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using System.Security.Claims;
using TravelAgency.Domain.Payment;
using TravelAgency.Service;

namespace TravelAgency.Web.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly StripeSettings _stripeSettings;

        public ShoppingCartsController(IShoppingCartService shoppingCartService, IOptions<StripeSettings> stripeSettings)
        {
            _shoppingCartService = shoppingCartService;
            _stripeSettings = stripeSettings.Value;
        }

        public IActionResult AddToCart(Guid packageId, int numberOfTravelers)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _shoppingCartService.AddToCart(packageId, userId, numberOfTravelers);
            return RedirectToAction("Index", "ShoppingCart");
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItems = _shoppingCartService.GetCartItems(userId);
            return View(cartItems);
        }

        [HttpPost]
        public IActionResult RemoveFromCart(Guid bookingId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _shoppingCartService.RemoveFromCart(bookingId, userId);
            return RedirectToAction("Index");
        }

        public IActionResult ConfirmCart(Guid bookingId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _shoppingCartService.ConfirmCart(bookingId,userId);
            return RedirectToAction("Index");
        }

        private bool Order()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = _shoppingCartService.Order(userId);
            return result;
        }

        public IActionResult SuccessPayment()
        {
            return View();
        }
        public IActionResult NotSuccessefullPayment()
        {
            return View();
        }

        public IActionResult PayOrder(string stripeEmail, string stripeToken)
        {
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
            var customerService = new CustomerService();
            var chargeService = new ChargeService();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var order = this._shoppingCartService.GetCartItems(userId);

            var customer = customerService.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = chargeService.Create(new ChargeCreateOptions
            {
                Amount = (Convert.ToInt32(order.Sum(b => b.Price)) * 100),
                Description = "TravelAgency Application Payment",
                Currency = "eur",
                Customer = customer.Id
            });

            if (charge.Status == "succeeded")
            {
                this.Order();
                return RedirectToAction("SuccessPayment");

            }
            else
            {
                return RedirectToAction("NotSuccessefullPayment");
            }
        }
    }
}
