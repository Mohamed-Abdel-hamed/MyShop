using System.ComponentModel.DataAnnotations;

namespace MyShop.Entities.Models;

public class Category
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    [MaxLength(300)]
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }= DateTime.Now;
}
