using System.ComponentModel.DataAnnotations;

namespace Mini_Inventory_Management_System.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }

        public string ProductName {get;set;}

        public DateTime OrderDate { get;set; }
    }
}
