using MyShop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Application.ViewModels;
public class OrderDetailsViewModel
{
    public int Id { get; set; }
    public ProductViewModel Product { get; set; }

    public int Count { get; set; }

    public decimal Price { get; set; }
}
