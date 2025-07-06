using MyShop.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Application.ViewModels;
public class OrderHeaderViewModel
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; } 
    public string? OrderStatus { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime ShippingDate { get; set; } 
    public string? PaymentStatus { get; set; }
    public string? TrackingNumber { get; set; }
    public string? Carrier { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? SessionId { get; set; }
    public string? PaymentIntentId { get; set; }
    public string UserName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string PhoneNumber { get; set; }
}
