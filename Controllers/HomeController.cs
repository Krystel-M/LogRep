using LogRep.Data;
using LogRep.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace LogRep.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RecipeContext context;

        public HomeController(ILogger<HomeController> logger, RecipeContext Dbcontext)
        {
            _logger = logger;
            context = Dbcontext;
        }

        /* public IActionResult Index()
         {
             return View();
         }*/

        

        /*[HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }*/
        /*  public async Task<IActionResult> Log()
          {
              var recipes = await _context.Recipes.ToListAsync();
              return View(recipes);
          }*/
        public async Task<IActionResult> Log()
        {
            var recipes = await context.Recipes.ToListAsync();
            return View(recipes);
        }


        public async Task<IActionResult> Index(string searchString)
        {
            if (context.Recipes == null)
            {
                return Problem("Entity set 'RecipeContext.Recipe'  is null.");
            }

            var recipes = from r in context.Recipes
                          select r;

            if (!String.IsNullOrEmpty(searchString))
            {
                recipes = recipes.Where(s => s.Name!.Contains(searchString));
            }

            return View(await recipes.ToListAsync());
        }
        /*   public IActionResult Log()
           {
               return View();
           }*/

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Create()
        {
            return View(new Recipe());
        }


        [HttpPost]
        public async Task<IActionResult> Create(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                context.Add(recipe);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Log));
            }
            return View(recipe);
        }
        /*[HttpPost]
        public async Task<IActionResult> Create(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                if (recipe.Id != 0)
                {
                    _context.Update(recipe);
                }
                else
                {
                    _context.Add(recipe);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }*/

        /*       public async Task<IActionResult> Edit(int? id)
               {
                   if (id == null)
                   {
                       return NotFound();
                   }

                   var recipe = await context.Recipes.FindAsync(id);
                   if (recipe == null)
                   {
                       return NotFound();
                   }
                   return View(recipe);
               }

               [HttpPost]
               public async Task<IActionResult> Edit(int id, Recipe recipe)
               {
                   if (id != recipe.Id)
                   {
                       return NotFound();
                   }

                   if (ModelState.IsValid)
                   {
                       context.Update(recipe);
                       await context.SaveChangesAsync();
                       return RedirectToAction(nameof(Index));
                   }
                   return View(recipe);
               }*/
        /*        public IActionResult Edit(int id)
                {
                    Recipe recipe = context.Recipes.FirstOrDefault(x => x.Id == id);
                    return View(recipe);
                }

                [HttpPost]
                *//*        public async Task<IActionResult> Edit(Recipe recipe)
                        {
                            if (ModelState.IsValid)
                            {
                                context.Update(recipe);
                                await context.SaveChangesAsync();
                                return RedirectToAction(nameof(Log));
                            }
                            return View(recipe);
                        }
                */
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                context.Update(recipe);
                await context.SaveChangesAsync(); // Update the changes in the database
                return RedirectToAction(nameof(Log));
            }

            return View(recipe);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await context.Recipes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        /*[HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipe = await context.Recipes.FindAsync(id);
            context.Recipes.Remove(recipe);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        /* public async Task<IActionResult> Delete(int id)
         {
             var log = await context.Recipes.FindAsync(id);
             if (log == null)
             {
                 return NotFound();
             }

             context.Recipes.Remove(log);
             await context.SaveChangesAsync();

             return RedirectToAction(nameof(Log));
         }*/
 
        public async Task<IActionResult> Delete(int id)
        {
            var log = await context.Recipes.FindAsync(id);
            if (log == null)
            {
                return NotFound();
            }

            context.Recipes.Remove(log);
            await context.SaveChangesAsync();

            /*return RedirectToAction("Log", "Home");*/
            return RedirectToAction(nameof(Log));
        }


    }
}
