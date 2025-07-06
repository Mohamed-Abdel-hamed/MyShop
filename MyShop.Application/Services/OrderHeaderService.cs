
using MyShop.Application.ViewModels;
using MyShop.Entities.Consts;
using MyShop.Entities.Models;
using MyShop.Entities.Repositories;

namespace MyShop.Application.Services;
public class OrderHeaderService(IUnitOfWork unitOfWork) : IOrderHeaderService
{
    private readonly IUnitOfWork _unitOfWork=unitOfWork;


    public OrderHeader? GetById(int id)
    {
        var orderHeader=_unitOfWork.OrderHeaders.GetById(id);
        if (orderHeader is null)
            return null;
        return orderHeader;
    }
    public OrderHeaderViewModel? Find(int id)
    {
        var orderHeader = _unitOfWork.OrderHeaders.Find(o => o.Id == id);
        if (orderHeader is null)
            return null;

        OrderHeaderViewModel viewModel = new()
        {
            Id = orderHeader.Id,
            OrderDate = orderHeader.OrderDate,
            OrderStatus = orderHeader.OrderStatus,
            TotalPrice = orderHeader.TotalPrice,
            ShippingDate = orderHeader.ShippingDate,
            PaymentStatus = orderHeader.PaymentStatus,
            TrackingNumber = orderHeader.TrackingNumber,
            Carrier = orderHeader.Carrier,
            PaymentDate = orderHeader.PaymentDate,
            SessionId = orderHeader.SessionId,
            PaymentIntentId = orderHeader.PaymentIntentId,
            UserName = orderHeader.UserName,
            Address = orderHeader.Address,
            City = orderHeader.City,
            PhoneNumber = orderHeader.PhoneNumber
        };
        return viewModel;
    }
    public OrderHeader Add(string userId,OrderHeaderFormModel model)
    {
        OrderHeader orderHeader = new()
        {
            OrderStatus = OrderStatus.Pending,
            PaymentStatus = PaymentStatus.Pending,
            TotalPrice=model.TotalPrice,
            UserId=userId,
            UserName=model.UsersViewModel.Name,
            City=model.UsersViewModel.City,
            Address=model.UsersViewModel.Address,
            PhoneNumber=model.UsersViewModel.PhoneNumber,
        };
        _unitOfWork.OrderHeaders.Add(orderHeader);
        _unitOfWork.Complete();
        return orderHeader;
    }

    public void UpdateOrderHeader(int id,OrderHeaderFormModel model)
    {
        var  currentOrderHeader = _unitOfWork.OrderHeaders.GetById(id);

        if (currentOrderHeader is null)
            return;

           currentOrderHeader.OrderStatus = model.OrderStatus;
           currentOrderHeader.TotalPrice = model.TotalPrice;
           currentOrderHeader.PaymentStatus = model.PaymentStatus;
           currentOrderHeader.TrackingNumber = model.TrackingNumber;
           currentOrderHeader.Carrier = model.Carrier;
           currentOrderHeader.ShippingDate = model.ShippingDate;
           currentOrderHeader.SessionId = model.SessionId;
           currentOrderHeader.PaymentIntentId = model.PaymentIntentId;
           currentOrderHeader.User.Name = model.UsersViewModel.Name;
           currentOrderHeader.User.Address = model.UsersViewModel.Address;
           currentOrderHeader.User.City = model.UsersViewModel.City;
           currentOrderHeader.User.PhoneNumber = model.UsersViewModel.PhoneNumber;
        
        _unitOfWork.Complete(); 
    }
    public void UpdateOrderStatus(int id, string orderStatus, string? paymentStatus, string? paymentIntentId)
    {
        var currentOrderHeader = _unitOfWork.OrderHeaders.GetById(id);


        if (currentOrderHeader is null)
            return;

        currentOrderHeader.OrderStatus = orderStatus;

        if (!string.IsNullOrWhiteSpace(paymentStatus))
        {
            currentOrderHeader.PaymentStatus = paymentStatus;
            currentOrderHeader.PaymentIntentId = paymentIntentId;
        }
        _unitOfWork.Complete();
    }
    public void UpdatePaymentInOrder(int id,string SessionId)
    {
        var orderHeader = _unitOfWork.OrderHeaders.GetById(id);
        orderHeader!.SessionId= SessionId;
        _unitOfWork.Complete();
    }

    public void UpdateOrderShip(int id, string trackingNumber, string carrier, string orderStatus)
    {
        var orderHeader = _unitOfWork.OrderHeaders.GetById(id);
        if (orderHeader is null)
            return;
        orderHeader.TrackingNumber = trackingNumber;
        orderHeader.Carrier = carrier;
        orderHeader.OrderStatus = orderStatus;
        orderHeader.ShippingDate = DateTime.Now;
        _unitOfWork.Complete();
    }

    public void UpdatePaymentIntentIdOrder(int id, string paymentIntentId)
    {
        var orderHeader = _unitOfWork.OrderHeaders.GetById(id);
        orderHeader!.PaymentIntentId = paymentIntentId;
        _unitOfWork.Complete();
    }
}
