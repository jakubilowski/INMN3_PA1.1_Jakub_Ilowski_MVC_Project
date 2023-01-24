using Microsoft.AspNetCore.Mvc;
using webApplication.Data;
using webApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace webApplication.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ShopContext _context;

        public ProductsController(ShopContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(string searchString, string Sorting_Order)
        {
            var products = await _context.Product.ToListAsync();

            if (!string.IsNullOrEmpty(searchString))
                products = products.Where(p => p.NazwaProduktu.Contains(searchString)).ToList();

            ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "Name_Description" : "";

            switch (Sorting_Order)
            {
                case "Name_Description":
                    products = products.OrderBy(stu => stu.NazwaProduktu).ToList();
                    break;
            }

            return View(products);

            // return View(await _context.Product.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id, NazwaProduktu, Model, Ilosc, Cena, Rodzaj")]
            Product product
            )
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Product == null)
                return NotFound();

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
                return NotFound();
            else
                return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.Product == null)
                return NotFound();

            var produkt = await _context.Product.FindAsync(id);

            if (produkt != null)
                _context.Product.Remove(produkt);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Product == null)
                return NotFound();

            var produkt = await _context.Product.FindAsync(id);

            if (produkt == null)
                return NotFound();
            else
                return View(produkt);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id, NazwaProduktu, Model, Ilosc, Cena, Rodzaj")]
            Product produkt
            )
        {
            if (id != produkt.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produkt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_context.Product.Any(e => e.Id == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(produkt);
        }
    }
}
