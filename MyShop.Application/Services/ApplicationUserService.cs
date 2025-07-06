using Microsoft.AspNetCore.Identity;
using MyShop.Entities.Models;
namespace MyShop.Application.Services;
public class ApplicationUserService(UserManager<ApplicationUser> userManager) : IApplicationUserService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    public async Task<ApplicationUser?> GetById(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
            return null;
        return user;
    }
}