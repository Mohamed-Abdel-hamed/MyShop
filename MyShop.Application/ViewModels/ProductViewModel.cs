﻿
namespace MyShop.Application.ViewModels;
public class ProductViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public decimal Price { get; set; }
    public string CategoryName { get; set; }
    public DateTime CreateTime { get; set; }
}
