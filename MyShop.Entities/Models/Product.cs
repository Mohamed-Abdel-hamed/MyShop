
using System.ComponentModel.DataAnnotations;

namespace MyShop.Entities.Models;
public class Product
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    [MaxLength(300)]
    public string Description { get; set; }
    public decimal Price { get; set; } = 0;
    public string ImageUrl { get; set; }
    public int CategoryId { get; set; }
    public DateTime CreateTime { get; set; }= DateTime.Now;
    public Category Category { get; set; }

}
