using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bookietry.Models;

namespace Bookietry.Controllers
{
    public class BooksSellersController : Controller
    {
        private readonly BookieDbContext _context;

        public BooksSellersController(BookieDbContext context)
        {
            _context = context;
        }

        // GET: BooksSellers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sellers.ToListAsync());
        }

        // GET: BooksSellers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booksSeller = await _context.Sellers
                .FirstOrDefaultAsync(m => m.Seller_id == id);
            if (booksSeller == null)
            {
                return NotFound();
            }

            return View(booksSeller);
        }

        // GET: BooksSellers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BooksSellers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Seller_id,Seller_Name,Seller_Number,Seller_email,Seller_address")] BooksSeller booksSeller)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booksSeller);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(booksSeller);
        }

        // GET: BooksSellers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booksSeller = await _context.Sellers.FindAsync(id);
            if (booksSeller == null)
            {
                return NotFound();
            }
            return View(booksSeller);
        }

        // POST: BooksSellers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Seller_id,Seller_Name,Seller_Number,Seller_email,Seller_address")] BooksSeller booksSeller)
        {
            if (id != booksSeller.Seller_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booksSeller);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BooksSellerExists(booksSeller.Seller_id))
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
            return View(booksSeller);
        }

        // GET: BooksSellers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booksSeller = await _context.Sellers
                .FirstOrDefaultAsync(m => m.Seller_id == id);
            if (booksSeller == null)
            {
                return NotFound();
            }

            return View(booksSeller);
        }

        // POST: BooksSellers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booksSeller = await _context.Sellers.FindAsync(id);
            _context.Sellers.Remove(booksSeller);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BooksSellerExists(int id)
        {
            return _context.Sellers.Any(e => e.Seller_id == id);
        }
    }
}
