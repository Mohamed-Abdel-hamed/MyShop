
namespace MyShop.Entities.Models;
public class OrderHeader
{
    public int Id { get; set; }

    public DateTime OrderDate { get; set; } = DateTime.Now;

    public string? OrderStatus { get; set; }

    public decimal TotalPrice { get; set; }
    public DateTime ShippingDate { get; set; }

    public string? PaymentStatus { get; set; }
    public string? TrackingNumber { get; set; }
    public string? Carrier { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.Now;

    // The session ID and PaymentIntentId is used to payment in  stripe.
    public string? SessionId { get; set; }
    public string? PaymentIntentId { get; set; }

    // user info
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
    public string? UserName { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? PhoneNumber { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }

}
