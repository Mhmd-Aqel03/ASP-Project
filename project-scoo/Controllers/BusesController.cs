using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project_scoo.Data;
using project_scoo.Models;

namespace project_scoo.Controllers
{
    public class BusesController : Controller
    {
        private readonly SystemDbContext _context;

        public BusesController(SystemDbContext context)
        {
            _context = context;
        }

        // GET: Buses
        public async Task<IActionResult> Index()
        {
              return _context.Busses != null ? 
                          View(await _context.Busses.ToListAsync()) :
                          Problem("Entity set 'SystemDbContext.Busses'  is null.");
        }

        // GET: Buses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Busses == null)
            {
                return NotFound();
            }

            var bus = await _context.Busses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bus == null)
            {
                return NotFound();
            }

            return View(bus);
        }

        // GET: Buses/Create
        public IActionResult Create()
        {
            ViewBag.Trips = _context.Trips.ToList();
            return View();
        }

        // POST: Buses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection form)
        {

            string capName = form["CapName"];
            int numofseats = int.Parse(form["NumOfSeats"]);
            int TripId = int.Parse(form["TripId"]);

            Bus Bus = new Bus();
            Bus.CapName = capName;
            Bus.NumOfSeats = numofseats;
            Bus.Trip = _context.Trips.Find(TripId);

            _context.Busses.Add(Bus);
            _context.SaveChanges();

            return RedirectToAction("Index");




        }

        // GET: Buses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Busses == null)
            {
                return NotFound();
            }

            var bus = await _context.Busses.FindAsync(id);
            if (bus == null)
            {
                return NotFound();
            }
            return View(bus);
        }

        // POST: Buses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CapName,NumOfSeats")] Bus bus)
        {
            if (id != bus.Id)
            {
                return NotFound();
            }



            _context.Update(bus);
            await _context.SaveChangesAsync();  


            return RedirectToAction(nameof(Index));

        }

        // GET: Buses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Busses == null)
            {
                return NotFound();
            }

            var bus = await _context.Busses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bus == null)
            {
                return NotFound();
            }

            return View(bus);
        }

        // POST: Buses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Busses == null)
            {
                return Problem("Entity set 'SystemDbContext.Busses'  is null.");
            }
            var bus = await _context.Busses.FindAsync(id);
            if (bus != null)
            {
                _context.Busses.Remove(bus);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusExists(int id)
        {
          return (_context.Busses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
