using Microsoft.AspNetCore.Mvc;
using MyShop.Entities.Consts;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Application.ViewModels;
public class CategoryFormModel
{
    public int Id { get; set; }
    [StringLength(100, MinimumLength = 3, ErrorMessage =Errors.MaxMinLength)]
    [Remote("AllowItem", null!, AdditionalFields = "Id", ErrorMessage = Errors.Duplicated)]
    public string Name { get; set; }
    [StringLength(100, MinimumLength = 3, ErrorMessage = Errors.MaxMinLength)]
    public string Description { get; set; }
}
