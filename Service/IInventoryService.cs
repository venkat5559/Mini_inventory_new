using Mini_Inventory_Management_System.Models;

namespace Mini_Inventory_Management_System.Service
{
    public interface IInventoryService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task PlaceOrderAsync(int productId, int quantity, string name);
        Task<List<Order>> GetAllOrdersAsync(); // New method for getting orders
    }

}
