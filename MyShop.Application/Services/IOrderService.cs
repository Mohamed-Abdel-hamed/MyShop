using MyShop.Application.ViewModels;

namespace MyShop.Application.Services;
public interface IOrderService
{
    IEnumerable<OrderViewModel> GetAll();
    object GetOrders(IQueryable<OrderViewModel> orders, int skip, int pageSize);
    IQueryable<OrderViewModel> GetQueryable();
    void UpdateDetails(OrderViewModel order);
}
