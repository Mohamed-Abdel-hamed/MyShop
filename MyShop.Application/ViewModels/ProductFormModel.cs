using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyShop.Entities.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Application.ViewModels;
public class ProductFormModel
{
    public int Id { get; set; }
    [StringLength(100, MinimumLength = 3, ErrorMessage = Errors.MaxMinLength)]
    public string Name { get; set; }
    [StringLength(300, MinimumLength = 3, ErrorMessage = Errors.MaxMinLength)]
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public IFormFile? Image {  get; set; }
    [Display(Name ="Category")]
    public int SelectedCategory { get; set; }
    public IEnumerable<SelectListItem>? Categories { get; set; }
}
