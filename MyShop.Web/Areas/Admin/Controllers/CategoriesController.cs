
using Microsoft.AspNetCore.Mvc;
using MyShop.Application.Services;
using MyShop.Application.ViewModels;

namespace MyShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController(ICategoryService categoryService) : Controller
    {
        private readonly ICategoryService _categoryService=categoryService;
        public IActionResult Index()
        {
            return View(_categoryService.GetAll());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryFormModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _categoryService.Add(model);

            TempData["create"] = "item has created successfully";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {

            var category = _categoryService.GetById(id);

            if (category == null)
                return NotFound();
            CategoryFormModel viewModel = new()
            {
                Id = id,
                Name = category.Name,
                Description = category.Description
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryFormModel model)
        {
            if (!ModelState.IsValid)

                return View(model);

            var category = _categoryService.GetById(model.Id);

            if (category == null)

                return NotFound();

            _categoryService.Edit(model);

           TempData["update"] = "item has updated successfully";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {

            var category = _categoryService.GetById(id);

            if (category == null)
                return NotFound();
            CategoryFormModel viewModel = new()
            {
                Id = id,
                Name = category.Name,
                Description = category.Description
            };

            return View(viewModel);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategory(int id)
        {
            var category = _categoryService.GetById(id);

            if (category == null)

                return NotFound();


            _categoryService.Delete(category.Id);

            TempData["delete"] = "item has deleted successfully";

            return RedirectToAction(nameof(Index));
        }
        public IActionResult AllowItem(CategoryFormModel model)
        {
            var isAllow= _categoryService.AllowItem(model.Id,model.Name);
            return Json(isAllow);
        }
    }
}
