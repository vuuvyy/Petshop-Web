using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Petland_Shop.Models;

namespace Petland_Shop.Controllers
{
    public class SearchController : Controller
    {
        private readonly DbMarketsContext _context;

        public SearchController(DbMarketsContext context)
        {
            _context = context;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        //GET: /<controller>/
        [HttpPost]
        public IActionResult FindProduct(string keyword)
        {
            //var ls = _context.Products.AsNoTracking()
            //                      .Where(x => x.ProductName.Contains(keyword))
            //                      .OrderByDescending(x => x.ProductName);
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListProductsSearchPartial", null);
            }
            var ls = _context.Products.AsNoTracking()
                                  .Where(x => x.ProductName.Contains(keyword))
                                  .OrderByDescending(x => x.ProductName).ToList();


            if (ls == null)
            {
                return PartialView("ListProductsSearchPartial", null);
            }
            else
            {
                return PartialView("ListProductsSearchPartial", ls);
            }
        }
    }
}
