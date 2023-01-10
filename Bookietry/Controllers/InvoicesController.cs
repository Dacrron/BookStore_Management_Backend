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
    public class InvoicesController : Controller
    {
        private readonly BookieDbContext _context;

        public InvoicesController(BookieDbContext context)
        {
            _context = context;
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            var bookieDbContext = _context.Invoices.Include(i => i.forkey3);
            return View(await bookieDbContext.ToListAsync());
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.forkey3)
                .FirstOrDefaultAsync(m => m.invoice_id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoices/Create
        public IActionResult Create()
        {
            ViewData["Book_Id"] = new SelectList(_context.Books, "Book_Id", "Book_name");
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("invoice_id,userID,Book_Id,quantity,invoice_name,invoice_phone,invoice_mail,invoice_address")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoice);
                await _context.SaveChangesAsync();
                if (InventoryExists(invoice.Book_Id))
                {
                    var inventories = _context.Inventories.Find(invoice.Book_Id);
                    if (invoice.quantity > inventories.quantity)
                    {
                        ViewData["Book_Id"] = new SelectList(_context.Books, "Book_Id", "Book_name", invoice.Book_Id);
                        return View(invoice);
                    }
                    inventories.quantity -= invoice.quantity;
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
            }
            ViewData["Book_Id"] = new SelectList(_context.Books, "Book_Id", "Book_name", invoice.Book_Id);
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            ViewData["Book_Id"] = new SelectList(_context.Books, "Book_Id", "Book_name", invoice.Book_Id);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("invoice_id,userID,Book_Id,quantity,invoice_name,invoice_phone,invoice_mail,invoice_address")] Invoice invoice)
        {
            if (id != invoice.invoice_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Add(invoice);
                await _context.SaveChangesAsync();
                if (InventoryExists(invoice.Book_Id))
                {
                    //var inventories = _context.Inventories.Find(invoice.Book_Id);
                    //if (invoice.quantity > inventories.quantity)
                    //{
                    //    ViewData["Book_Id"] = new SelectList(_context.Books, "Book_Id", "Book_name", invoice.Book_Id);
                    //    return View(invoice);
                    //}
                    //inventories.quantity -= invoice.quantity;

                    try
                    {
                        _context.Update(invoice);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!InvoiceExists(invoice.invoice_id))
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
            }
            ViewData["Book_Id"] = new SelectList(_context.Books, "Book_Id", "Book_Id", invoice.Book_Id);
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.forkey3)
                .FirstOrDefaultAsync(m => m.invoice_id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceExists(int id)
        {
            return _context.Invoices.Any(e => e.invoice_id == id);
        }
        private bool InventoryExists(int id)
        {
            return _context.Inventories.Any(e => e.Book_Id == id);
        }
    }
}
