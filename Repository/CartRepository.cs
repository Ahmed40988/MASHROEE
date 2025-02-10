using MASHROEE.IRepository;
using MASHROEE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class CartRepository : ICartRepository
{
    private readonly UserManager<Applicationuser> userManager;
    private readonly IProductRepository productRepository;
    private readonly MASHROEEDbContext context;

    public CartRepository(UserManager<Applicationuser> userManager, IProductRepository productRepository, MASHROEEDbContext context)
    {
        this.userManager = userManager;
        this.productRepository = productRepository;
        this.context = context;
    }

    public  async Task<List<Cart>> GetAllCarts()
    {
        var carts = context.Cart.Include(c=>c.CartItems).ToList();
        return carts;
    }

    public async Task AddToCartAsync(string userId, int productId, int quantity)
    {
        var cart = await context.Cart
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null)
        {
            cart = new Cart { UserId = userId };
            context.Cart.Add(cart);
        }

        cart.CartItems = cart.CartItems ?? new List<Cartitem>();

        var cartItem = cart.CartItems.FirstOrDefault(item => item.ProductId == productId);
            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                var product = productRepository.GetProductById(productId);
                cart.CartItems.Add(new Cartitem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    ProductName = product.Name,
                    Price = product.Price,
                    image = product.image,
                });
            }
            await context.SaveChangesAsync();
        }

    

    public async Task RemoveFromCartAsync(string userId, int productId)
    {
        var cart = await context.Cart
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart != null)
        {
            var cartItem = cart.CartItems.FirstOrDefault(item => item.ProductId == productId);
            if (cartItem != null)
            {
                cart.CartItems.Remove(cartItem);
                await context.SaveChangesAsync();
            }
        }
    }

    public async Task UpdateCartItemQuantityAsync(string userId, int productId, int quantity)
    {
        var cart = await context.Cart
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart != null)
        {
            var cartItem = cart.CartItems.FirstOrDefault(item => item.ProductId == productId);
            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                await context.SaveChangesAsync();
            }
        }
    }

    public async Task<List<Cartitem>> GetCartItemsAsync(string userId)
    {
        var cart = await context.Cart
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        return cart?.CartItems.ToList() ?? new List<Cartitem>();
    }

    public async Task ClearCartAsync(string userId)
    {
        var cart = await context.Cart
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart != null)
        {
            context.Cartitems.RemoveRange(cart.CartItems);
            await context.SaveChangesAsync();
        }
    }

    public async Task<decimal> GetCartTotalAsync(string userId)
    {
        var cart = await context.Cart
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null || !cart.CartItems.Any())
            return 0;

        return cart.CartItems.Sum(item => item.Quantity * item.Price);
    }

}