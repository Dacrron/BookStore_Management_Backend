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
    public class PurchasesController : Controller
    {
        private readonly BookieDbContext _context;

        public PurchasesController(BookieDbContext context)
        {
            _context = context;
        }

        // GET: Purchases
        public async Task<IActionResult> Index()
        {
            var bookieDbContext = _context.Purchases.Include(p => p.forkey1).Include(p => p.forkey2);
            return View(await bookieDbContext.ToListAsync());
        }

        // GET: Purchases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchases
                .Include(p => p.forkey1)
                .Include(p => p.forkey2)
                .FirstOrDefaultAsync(m => m.purchase_id == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        // GET: Purchases/Create
        public IActionResult Create()
        {
            ViewData["Seller_id"] = new SelectList(_context.Sellers, "Seller_id", "Seller_Name");
            ViewData["Book_Id"] = new SelectList(_context.Books, "Book_Id", "Book_name");
            return View();
        }

        // POST: Purchases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("purchase_id,Seller_id,Book_Id,Book_name,purchase_quantity,purchase_date,purchase_cost")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchase);
                await _context.SaveChangesAsync();
                if (InventoryExists(purchase.Book_Id))
                {
                    var inventories = _context.Inventories.Find(purchase.Book_Id);
                    inventories.quantity += purchase.purchase_quantity;
                    try
                    {
                        _context.Update(inventories);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!InventoryExists(inventories.Book_Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                return RedirectToAction(nameof(Index));
                //return RedirectToAction(nameof(Index));
            }
            ViewData["Seller_id"] = new SelectList(_context.Sellers, "Seller_id", "Seller_Name", purchase.Seller_id);
            ViewData["Book_Id"] = new SelectList(_context.Books, "Book_Id", "Book_name", purchase.Book_Id);
            return View(purchase);
        }

        // GET: Purchases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchases.FindAsync(id);
            if (purchase == null)
            {
                return NotFound();
            }
            ViewData["Seller_id"] = new SelectList(_context.Sellers, "Seller_id", "Seller_Name", purchase.Seller_id);
            ViewData["Book_Id"] = new SelectList(_context.Books, "Book_Id", "Book_name", purchase.Book_Id);
            return View(purchase);
        }

        // POST: Purchases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("purchase_id,Seller_id,Book_Id,Book_name,purchase_quantity,purchase_date,purchase_cost")] Purchase purchase)
        {
            if (id != purchase.purchase_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseExists(purchase.purchase_id))
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
            ViewData["Seller_id"] = new SelectList(_context.Sellers, "Seller_id", "Seller_Name", purchase.Seller_id);
            ViewData["Book_Id"] = new SelectList(_context.Books, "Book_Id", "Book_name", purchase.Book_Id);
            return View(purchase);
        }

        // GET: Purchases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchases
                .Include(p => p.forkey1)
                .Include(p => p.forkey2)
                .FirstOrDefaultAsync(m => m.purchase_id == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchase = await _context.Purchases.FindAsync(id);
            _context.Purchases.Remove(purchase);
            await _context.SaveChangesAsync();
           
                await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseExists(int id)
        {
            return _context.Purchases.Any(e => e.purchase_id == id);
        }
        private bool InventoryExists(int? id)
        {
            return _context.Inventories.Any(e => e.Book_Id == id);
        }
    }
}
