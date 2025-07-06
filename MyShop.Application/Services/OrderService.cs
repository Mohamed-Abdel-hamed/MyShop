
using MyShop.Application.ViewModels;
using MyShop.Entities.Repositories;

namespace MyShop.Application.Services;
public class OrderService(IUnitOfWork unitOfWork) : IOrderService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public IEnumerable<OrderViewModel> GetAll()
    {
        return _unitOfWork.OrderHeaders.GetQueryable()
             .Select(header => new OrderViewModel
             {
                 OrderHeaderViewModel = new OrderHeaderViewModel
                 {
                     Id = header.Id,
                     OrderDate = header.OrderDate,
                     OrderStatus = header.OrderStatus,
                     TotalPrice = header.TotalPrice,
                     ShippingDate = header.ShippingDate,
                     PaymentStatus = header.PaymentStatus,
                     TrackingNumber = header.TrackingNumber,
                     Carrier = header.Carrier,
                     PaymentDate = header.PaymentDate,
                     SessionId = header.SessionId,
                     PaymentIntentId = header.PaymentIntentId,
                     UserName = header.User.UserName,
                     Address = header.User.Address,
                     City = header.User.City,
                     PhoneNumber = header.PhoneNumber
                 },
                 OrderDetailsViewModel = header.OrderDetails.Select(detail => new OrderDetailsViewModel
                 {
                     Id = detail.Id,
                     Product = new ProductViewModel
                     {
                         Name = detail.Product.Name,
                         Price = detail.Product.Price,
                         CategoryName = detail.Product.Category.Name
                     },
                     Count = detail.Count,
                     Price = detail.Price,
                 }).ToList()
             }).ToList();
    }

    public object GetOrders(IQueryable<OrderViewModel> orders, int skip, int pageSize)
    {
       var mappingData = orders
             .Select(o => new
             {
                o.OrderHeaderViewModel.Id,
                o.OrderHeaderViewModel.OrderStatus,
                o.OrderHeaderViewModel.TotalPrice,
                o.OrderHeaderViewModel.UserName,
                o.OrderHeaderViewModel.PhoneNumber
             })
            .Skip(skip)
            .Take(pageSize)
            .ToList();
        var recordTotal = orders.Count();
        var jsonData = new { recordsFiltered = recordTotal, recordTotal, data = mappingData };
        return jsonData;
    }
    public IQueryable<OrderViewModel> GetQueryable()
    {
        return _unitOfWork.OrderHeaders.GetQueryable()
            .Select(header => new OrderViewModel
            {
                OrderHeaderViewModel = new OrderHeaderViewModel
                {
                    Id = header.Id,
                    OrderDate = header.OrderDate,
                    OrderStatus = header.OrderStatus,
                    TotalPrice = header.TotalPrice,
                    ShippingDate = header.ShippingDate,
                    PaymentStatus=header.PaymentStatus,
                    TrackingNumber =header.TrackingNumber,
                    Carrier =header.Carrier,
                    PaymentDate=header.PaymentDate,
                    SessionId=header.SessionId,
                     PaymentIntentId =header.PaymentIntentId,
                     UserName=header.User.UserName,
                     Address = header.User.Address,
                     City =header.User.City,
                      PhoneNumber=header.PhoneNumber
                        },
                OrderDetailsViewModel = header.OrderDetails.Select(detail => new OrderDetailsViewModel
                {
                    Id = detail.Id,
                    Product = new ProductViewModel
                    {
                        Name = detail.Product.Name,
                        Price = detail.Product.Price,
                        CategoryName=detail.Product.Category.Name
                    },
                    Count=detail.Count,
                    Price = detail.Price,
                }).ToList()
            });
    }
    public void UpdateDetails(OrderViewModel order)
    {
        var currentOrder =_unitOfWork.OrderHeaders.GetById(order.OrderHeaderViewModel.Id);
        if (currentOrder is null)
            return;
        currentOrder.UserName= order.OrderHeaderViewModel.UserName;
        currentOrder.Address = order.OrderHeaderViewModel.Address;
        currentOrder.PhoneNumber = order.OrderHeaderViewModel.PhoneNumber;
        currentOrder.City = order.OrderHeaderViewModel.City;
        if(order.OrderHeaderViewModel.Carrier != null)
            currentOrder.Carrier=order.OrderHeaderViewModel.Carrier;
        if (order.OrderHeaderViewModel.TrackingNumber != null)
            currentOrder.TrackingNumber = order.OrderHeaderViewModel.TrackingNumber;
        _unitOfWork.Complete();
    }

}
