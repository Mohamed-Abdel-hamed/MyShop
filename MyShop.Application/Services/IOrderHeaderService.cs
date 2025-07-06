using MyShop.Application.ViewModels;
using MyShop.Entities.Models;
using System.ComponentModel;

namespace MyShop.Application.Services;
public interface IOrderHeaderService
{
    OrderHeader? GetById(int id);
    OrderHeader Add(string userId, OrderHeaderFormModel model);
    OrderHeaderViewModel? Find(int id);
    void UpdateOrderHeader(int id,OrderHeaderFormModel model);
    void UpdateOrderStatus(int id, string orderStatus,string? paymentStatus, string? paymentIntentId);
    void UpdatePaymentInOrder(int id,string SessionId);
    void UpdatePaymentIntentIdOrder(int id, string paymentIntentId);
    void UpdateOrderShip(int id, string trackingNumber, string carrier,string orderStatus);
}
