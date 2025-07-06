using MyShop.Application.ViewModels;
using MyShop.Entities.Models;
using MyShop.Entities.Repositories;

namespace MyShop.Application.Services;
public class OrderDetailService(IUnitOfWork unitOfWork) : IOrderDetailService
{
    private readonly IUnitOfWork _unitOfWork=unitOfWork;


    public IEnumerable<OrderDetailsViewModel> GetOrderDetailsByOrderId(int orderId)
    {
        var orderDetails = _unitOfWork.OrderDetails.GetAll(o => o.OrderHeaderId == orderId, ["Product"]);
       return orderDetails.Select(detail => new OrderDetailsViewModel
        {
            Id = detail.Id,
            Product = new ProductViewModel
            {
                Name = detail.Product.Name,
                Price = detail.Product.Price,
            },
            Count = detail.Count,
            Price = detail.Price
        })
            .ToList();
    }
    public void Add(int productId, int orderHeaderId, decimal price, int count)
    {
       OrderDetail orderDetail=new()
       {
           ProductId=productId,
           OrderHeaderId=orderHeaderId,
           Price =price,
           Count = count,
       };
        _unitOfWork.OrderDetails.Add(orderDetail);
        _unitOfWork.Complete();

    }

    public void UpdateOrderDetail(int id,OrderDetailFormModel model)
    {
        var currentOrderDetail = _unitOfWork.OrderDetails.GetById(id);
        if (currentOrderDetail is null)
            return;
        currentOrderDetail.Price = model.Price;
        currentOrderDetail.Count = model.Count;

        _unitOfWork.Complete();
    }
}
