
using Microsoft.EntityFrameworkCore;
using Mini_Inventory_Management_System.Models;

namespace Mini_Inventory_Management_System.Service
{
    public class InventoryService : IInventoryService
    {
        private static List<Product> _products = new List<Product>();
        
        private static List<Order> _orders = new List<Order>();

        public Task<List<Product>> GetAllProductsAsync() => Task.FromResult(_products);

        public Task<Product> GetProductByIdAsync(int id) =>
            Task.FromResult(_products.FirstOrDefault(p => p.Id == id));

        public Task AddProductAsync(Product product)
        {
            product.Id = _products.Count + 1;  // Simple auto-increment logic
            _products.Add(product);
            return Task.CompletedTask;
        }

        public Task UpdateProductAsync(Product product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Stock = product.Stock;
            }
            return Task.CompletedTask;
        }

        public Task DeleteProductAsync(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _products.Remove(product);
            }
            return Task.CompletedTask;
        }

        public Task PlaceOrderAsync(int productId, int quantity, string name)
        {
            var product = _products.FirstOrDefault(p => p.Id == productId);
            if (product == null || product.Stock < quantity)
            {
                throw new InvalidOperationException("Insufficient stock for this order.");
            }

            product.Stock -= quantity;
            var order = new Order
            {
                Id = _orders.Count + 1,
                ProductId = productId,
                ProductName = product.Name,
                Quantity = quantity,
                TotalAmount = product.Price * quantity,
                OrderDate = DateTime.Now
            };
            _orders.Add(order);
            return Task.CompletedTask;
        }

        public Task<List<Order>> GetAllOrdersAsync() =>
            Task.FromResult(_orders);  // Return the list of orders
    }


}

