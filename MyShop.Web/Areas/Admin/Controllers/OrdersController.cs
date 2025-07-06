using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShop.Application.Services;
using MyShop.Application.ViewModels;
using MyShop.Entities.Consts;
using MyShop.Entities.Models;
using Stripe;
using System.Linq.Dynamic.Core;

namespace MyShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrdersController(IOrderService orderService,
        IOrderHeaderService orderHeaderService,
         IOrderDetailService orderDetailService) : Controller
    {
        private readonly IOrderService _orderService = orderService;
        private readonly IOrderHeaderService _orderHeaderService = orderHeaderService;
        private readonly IOrderDetailService _orderDetailService = orderDetailService;
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GetOrders()
        {
            var skip = int.Parse(Request.Form["start"]!);
            var pageSize = int.Parse(Request.Form["length"]!);
            var sortColumnIndex = Request.Form["order[0][column]"]!;
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][name]"]!;
            var sortColumnDirection = Request.Form["order[0][dir]"];
            var searchValue = Request.Form["search[value]"];

            IQueryable<OrderViewModel> orders = _orderService.GetQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                orders = orders.Where(o =>
                    o.OrderHeaderViewModel.UserName!.Contains(searchValue!) ||
                    o.OrderHeaderViewModel.OrderStatus!.Contains(searchValue!));
            }
            var orderByColumn = $"OrderHeaderViewModel.{sortColumn} {sortColumnDirection}";
            orders = orders.OrderBy(orderByColumn);

            return Ok(_orderService.GetOrders(orders, skip, pageSize));
        }
        public IActionResult Details(int id)
        {
            var header = _orderHeaderService.GetById(id); 

            if (header is null)
                return NotFound();

            var orderViewModel = new OrderViewModel
            {
                OrderHeaderViewModel = _orderHeaderService.Find(id)!,
                OrderDetailsViewModel = _orderDetailService.GetOrderDetailsByOrderId(id)
            };

            return View(orderViewModel); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateDetails(OrderViewModel model)
        {
            var order = _orderHeaderService.GetById(model.OrderHeaderViewModel.Id); 

            if (order is null)
                return NotFound();
            if(!ModelState.IsValid)
            {
                model.OrderHeaderViewModel = _orderHeaderService.Find(model.OrderHeaderViewModel.Id)!;
                model.OrderDetailsViewModel = _orderDetailService.GetOrderDetailsByOrderId(model.OrderHeaderViewModel.Id);
                return View("Details", model);
            }
                _orderService.UpdateDetails(model);
            TempData["update"] = "item has updated successfully";

            return RedirectToAction("Details", "Orders", new {id=order.Id}); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StartProcess(OrderViewModel model)
        {
            var order = _orderHeaderService.GetById(model.OrderHeaderViewModel.Id);

            if (order is null)
                return NotFound();
            _orderHeaderService.UpdateOrderStatus(order.Id, OrderStatus.Processing, null,null);
            TempData["update"] = "item has process successfully";

            return RedirectToAction("Details", "Orders", new { id = order.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StartShip(OrderViewModel model)
        {
            var order = _orderHeaderService.GetById(model.OrderHeaderViewModel.Id);

            if (order is null)
                return NotFound();
            if (!ModelState.IsValid)
            {
                model.OrderHeaderViewModel = _orderHeaderService.Find(model.OrderHeaderViewModel.Id)!;
                model.OrderDetailsViewModel = _orderDetailService.GetOrderDetailsByOrderId(model.OrderHeaderViewModel.Id);
                return View("Details", model);
            }

            _orderHeaderService.UpdateOrderShip(order.Id,model.OrderHeaderViewModel.TrackingNumber!,model.OrderHeaderViewModel.Carrier!, OrderStatus.Shipped);
            TempData["update"] = "item has shipped successfully";

            return RedirectToAction("Details", "Orders", new { id = order.Id });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cancel(OrderViewModel model)
        {
            var order = _orderHeaderService.GetById(model.OrderHeaderViewModel.Id);

            if (order is null)
                return NotFound();
            if(order.PaymentStatus == PaymentStatus.Approve)
            {
                RefundCreateOptions refundCreateOptions = new()
                {
                    Reason = RefundReasons.RequestedByCustomer,
                    PaymentIntent = order.PaymentIntentId
                };
                RefundService service = new();
                Refund refund = service.Create(refundCreateOptions);
                _orderHeaderService.UpdateOrderStatus(order.Id, OrderStatus.Cancelled, PaymentStatus.Refunded,null);
            }
            else
            {
                _orderHeaderService.UpdateOrderStatus(order.Id, OrderStatus.Cancelled, PaymentStatus.Cancelled, null);
            }
            TempData["update"] = "item has cancelled successfully";

            return RedirectToAction("Details", "Orders", new { id = order.Id });
        }
    } 
}
