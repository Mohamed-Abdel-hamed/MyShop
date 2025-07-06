using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Entities.Consts;
public static class OrderStatus
{
    public const string Pending = "Pending";
    public const string Approve = "Approve";
    public const string Processing = "Processing";
    public const string Shipped = "Shipped";
    public const string Delivered = "Delivered";
    public const string Cancelled = "Cancelled";
    public const string Refunded = "Refunded";
}
