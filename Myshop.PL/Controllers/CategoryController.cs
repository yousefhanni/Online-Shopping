using Microsoft.AspNetCore.Mvc;
using Myshop.DAL.Data.Context;
using Myshop.DAL.Models;

namespace Myshop.PL.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        //Just Display Data
        // /Category/Index
        public IActionResult Index()
        {
            var categories = _dbContext.Categories.ToList();//retrieves all categories from Database
            return View(categories);//(Binding => send info(categories) from Action of Index to View of Index)
        }

        [HttpGet]
        //It returns a view for creating a category.
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
          if (ModelState.IsValid)//Server side validation
          {
              _dbContext.Categories.Add(category);
              _dbContext.SaveChanges(); 
              return RedirectToAction(nameof(Index)); // Redirect to Index action after successful creation
          }
          return View(category); // Return the same view with validation errors if data is invalid
          
        }
        
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return BadRequest();

            var category = _dbContext.Categories.Find(id);
            if (category == null)
                return NotFound();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid) //Server side Validation 
            {
                try
                {
                    _dbContext.Categories.Update(category);
                    _dbContext.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while Editing the category.");
                }
            }
            return View(category);
        
            }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Edit(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var categoryToDelete = _dbContext.Categories.Find(id);
                if (categoryToDelete == null)
                {
                    return NotFound(); // Return 404 Not Found if the category with the given ID doesn't exist
                }
                _dbContext.Categories.Remove(categoryToDelete);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                ModelState.AddModelError(string.Empty, "An error occurred while Deleting the category.");
                return View(); // Return to the view with the error message
            }
        }

    }


}
