
namespace MyShop.Application.ViewModels;
public class OrderDetailFormModel
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int OrderHeaderId { get; set; }
    public int Count { get; set; }
    public decimal Price { get; set; }
}
