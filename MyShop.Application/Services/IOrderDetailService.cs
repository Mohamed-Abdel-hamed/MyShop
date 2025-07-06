using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MyShop.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Application.Services;
public interface IOrderDetailService
{
    IEnumerable<OrderDetailsViewModel> GetOrderDetailsByOrderId(int orderId);
    void Add(int productId,int orderHeaderId,decimal price,int count );
    void UpdateOrderDetail(int id,OrderDetailFormModel model);
}
