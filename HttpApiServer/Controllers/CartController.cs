using HttpModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HttpApiServer
{
    [Route("cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartService _service;

        public CartController(CartService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost("add_to_cart")]
        public async Task<ActionResult<ResponseModel<Product>>> AddToCart(Product product)
        {

            ;
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _service.AddProduct(userId, product.Id);
            ;
            return new ResponseModel<Product>() { Succeeded = true };
        }

        [Authorize]
        [HttpGet("get_cart")]
        public async Task<ActionResult<ResponseModel<Cart>>> GetCart()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var products = await _service.GetUserCart(userId);


            return new ResponseModel<Cart>()
            {
                Succeeded = true,
                Result = products
            };


        }
    }


}


