﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using Petland_Shop.Models;

namespace Petland_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminTinDangsController : Controller
    {
        private readonly DbMarketsContext _context;

        public AdminTinDangsController(DbMarketsContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminTinDangs
        public IActionResult Index(int? page)
        {
            var collection = _context.TinDangs.AsNoTracking().ToList();
            foreach (var item in collection)
            {
                if (item.CreatedDate == null)
                {
                    item.CreatedDate = DateTime.Now;
                    _context.Update(item);
                    _context.SaveChanges();
                }
            }

            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;
            var lsTinDangs = _context.TinDangs
                .AsNoTracking()
                .OrderBy(x => x.PostId);
            PagedList<TinDang> models = new PagedList<TinDang>(lsTinDangs, pageNumber, pageSize);

            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }

        // GET: Admin/AdminTinDangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TinDangs == null)
            {
                return NotFound();
            }

            var tinDang = await _context.TinDangs
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (tinDang == null)
            {
                return NotFound();
            }

            return View(tinDang);
        }

        // GET: Admin/AdminTinDangs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminTinDangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,Title,Scontents,Contents,Thumb,Published,Alias,CreatedDate,Author,AccountId,Tags,CatId,IsHot,IsNewfeed,MetaKey,MetaDesc,Views")] TinDang tinDang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tinDang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tinDang);
        }

        // GET: Admin/AdminTinDangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TinDangs == null)
            {
                return NotFound();
            }

            var tinDang = await _context.TinDangs.FindAsync(id);
            if (tinDang == null)
            {
                return NotFound();
            }
            return View(tinDang);
        }

        // POST: Admin/AdminTinDangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,Title,Scontents,Contents,Thumb,Published,Alias,CreatedDate,Author,AccountId,Tags,CatId,IsHot,IsNewfeed,MetaKey,MetaDesc,Views")] TinDang tinDang)
        {
            if (id != tinDang.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tinDang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TinDangExists(tinDang.PostId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tinDang);
        }

        // GET: Admin/AdminTinDangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TinDangs == null)
            {
                return NotFound();
            }

            var tinDang = await _context.TinDangs
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (tinDang == null)
            {
                return NotFound();
            }

            return View(tinDang);
        }

        // POST: Admin/AdminTinDangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TinDangs == null)
            {
                return Problem("Entity set 'DbMarketsContext.TinDangs'  is null.");
            }
            var tinDang = await _context.TinDangs.FindAsync(id);
            if (tinDang != null)
            {
                _context.TinDangs.Remove(tinDang);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TinDangExists(int id)
        {
          return (_context.TinDangs?.Any(e => e.PostId == id)).GetValueOrDefault();
        }
    }
}
