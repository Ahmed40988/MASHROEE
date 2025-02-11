using MASHROEE.IRepository;
using MASHROEE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity;
using System.Security.Claims;

namespace MASHROEE.Controllers
{
    public class CartController : Controller
    {
        private readonly UserManager<Applicationuser> userManager;
        private readonly IProductRepository productRepository;
        private readonly ICartRepository cartRepository;

     public CartController(UserManager<Applicationuser> userManager,IProductRepository productRepository, ICartRepository cartRepository)
        {
            this.userManager = userManager;
            this.productRepository = productRepository;
            this.cartRepository = cartRepository;
        }
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var items = await cartRepository.GetCartItemsAsync(userId); 
            return View(items);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productid, int quantity)
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userid))
            {
                return Json(new { success = false, message = "Please log in to add products to cart." });
            }

            var productDB = productRepository.GetProductById(productid);
            if (productDB != null && productDB.Quantity >= quantity)
            {
                await cartRepository.AddToCartAsync(userid, productid, quantity);
                return Json(new { success = true, message = "Product added to cart successfully." });
            }
            else
            {
                return Json(new { success = false, message = "The available quantity is not enough." });
            }
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmOrder()
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var cartItems = await cartRepository.GetCartItemsAsync(userid);

            foreach (var item in cartItems)
            {
                var product = productRepository.GetProductById(item.ProductId); 
                if (product != null && product.Quantity >= item.Quantity)
                {
                    product.Quantity-= item.Quantity;
                    await productRepository.UpdateProductAsync(product);
                }
                else
                {
                    TempData["Error"] = $"Insufficient Product for {item.ProductName}.";
                    return RedirectToAction("Index");
                }
            }
            TempData["Success"] = "Order confirmed successfully!";
            return RedirectToAction("Index");
        }




        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            await cartRepository.RemoveFromCartAsync(userId, productId); 
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ClearCart()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }
            await cartRepository.ClearCartAsync(userId);
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> UpdateCartItem(int productid,int quantity)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var productDB = productRepository.GetProductById(productid);
            if (productDB.Quantity >= quantity)
            {
            await cartRepository.UpdateCartItemQuantityAsync(userId, productid, quantity);
                productDB.Quantity -= quantity;
               await productRepository.UpdateProductAsync(productDB);
                return RedirectToAction("index");
            }
            else
                return Content("The available quantity is not enough");
        }


    }
}
