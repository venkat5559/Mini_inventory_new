using Microsoft.AspNetCore.Mvc;
using Mini_Inventory_Management_System.Service;

namespace Mini_Inventory_Management_System.Controllers
{
    public class OrderController : Controller
    {
        private readonly IInventoryService _inventoryService;

        public OrderController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        // Display the page to place an order
        public async Task<IActionResult> PlaceOrder()
        {
            var products = await _inventoryService.GetAllProductsAsync();
            return View(products);  // Pass the list of products to the view for selection
        }

        // Handle the order submission
        [HttpPost]
        public async Task<IActionResult> PlaceOrder(int productId, int quantity, string name)
        {
            if (quantity <= 0)
            {
                ModelState.AddModelError("", "Quantity must be greater than zero.");
                var products = await _inventoryService.GetAllProductsAsync();
                return View(products); // Re-display products with error message
            }

            try
            {
                // Try placing the order
                await _inventoryService.PlaceOrderAsync(productId, quantity, name);
                TempData["SuccessMessage"] = "Order placed successfully!";
                return RedirectToAction(nameof(OrderHistory));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message); // Error if stock is insufficient
                var products = await _inventoryService.GetAllProductsAsync();
                return View(products);
            }
        }

        // Display the list of orders
        public async Task<IActionResult> OrderHistory()
        {
            var orders = await _inventoryService.GetAllOrdersAsync();
            return View(orders);  // Pass the list of orders to the view
        }
    }

}
