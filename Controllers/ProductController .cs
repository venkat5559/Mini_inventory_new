using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mini_Inventory_Management_System.Models;
using Mini_Inventory_Management_System.Service;

namespace Mini_Inventory_Management_System.Controllers
{
    public class ProductController : Controller
    {
        private readonly IInventoryService _inventoryService;

        public ProductController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        // Display all products
        public async Task<IActionResult> Index()
        {
            var products = await _inventoryService.GetAllProductsAsync();
            return View(products);
        }

        // Add a new product
       public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _inventoryService.AddProductAsync(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // Edit a product
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _inventoryService.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                await _inventoryService.UpdateProductAsync(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // Delete a product
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _inventoryService.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _inventoryService.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
