﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using Petland_Shop.Models;


namespace Petland_Shop.Controllers
{
    public class ProductController : Controller
    {
        private readonly DbMarketsContext _context;
        public ProductController(DbMarketsContext context)
        {
            _context = context;
        }
        [Route("shop.html", Name = ("ShopProduct"))]
        public IActionResult Index(int? page, int CatID = 0)
        {
            try
            {
                var pageNumber = page == null || page <= 0 ? 1 : page.Value;
                var pageSize = 10;
                var lsTinDangs =  _context.Products
                    .AsNoTracking()
                    .Include(x => x.Cat)
                    .OrderBy(x => x.DateCreated);
                if (CatID != 0)
                {
                     lsTinDangs = _context.Products
                    .AsNoTracking()
                    .Where(x => x.CatId == CatID)
                    .Include(x => x.Cat)
                    .OrderBy(x => x.DateCreated);
                }
               
                PagedList<Product> models = new PagedList<Product>(lsTinDangs, pageNumber, pageSize);

                ViewBag.CurrentPage = pageNumber;
                ViewData["DanhMuc"] = new SelectList(_context.Categories, "CatId", "CatName");
                return View(models);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
            

        }
        [Route("danhmuc/{Alias}", Name = ("ListProduct"))]
        public IActionResult List(string Alias, int page = 1)
        {
            try
            {
                var pageSize = 10;
                var danhmuc = _context.Categories.AsNoTracking().SingleOrDefault(x => x.Alias == Alias);

                var lsTinDangs = _context.Products
                    .AsNoTracking()
                    .Where(x => x.CatId == danhmuc.CatId)
                    .OrderByDescending(x => x.DateCreated);
                PagedList<Product> models = new PagedList<Product>(lsTinDangs, page, pageSize);
                ViewBag.CurrentPage = page;
                ViewBag.CurrentCat = danhmuc;
                return View(models);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }


        }

        [Route("/{Alias}-{id}.html", Name = ("ProductDetails"))]
        public IActionResult Details(int id)
        {
            try
            {
                var product = _context.Products.Include(x => x.Cat).FirstOrDefault(x => x.ProductId == id);
                if (product == null)
                {
                    return RedirectToAction("Index");
                }
                var lsProduct = _context.Products
                    .AsNoTracking()
                    .Where(x => x.CatId == product.CatId && x.ProductId != id && x.Active == true)
                    .Take(4)
                    .OrderByDescending(x => x.DateCreated)
                    .ToList();
                ViewBag.SanPham = lsProduct;
                return View(product);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }


        }

        public IActionResult Filtter(int CatId = 0)
        {
            var url = $"shop.html?CatID={CatId}";
            if (CatId == 0)
            {
                url = $"shop.html";
            }
            return Json(new { status = "success", redirectUrl = url });
        }

    }
}
