using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia_HT.DAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace Pronia_HT.Areas.ProniaAdmin.Controllers
{

    [Area("ProniaAdmin")]
    public class SizeController : Controller
    {

      

        private readonly AppDbContext _context;
        public SizeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Models.Size> sizes = await _context.Sizes.ToListAsync();
            return View(sizes);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Models.Size size)
        {
           
            if(!ModelState.IsValid) return View(); 
            await _context.Sizes.AddAsync(size);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

        

        public async Task<IActionResult>Detail(int id)
        {
            Models.Size size = await _context.Sizes.FirstOrDefaultAsync(s=>s.Id == id);
            if (size == null) return NotFound();
            return View(size);
        }
        public async Task<IActionResult> Edit(int id)
        {
            Models.Size size = await _context.Sizes.FirstOrDefaultAsync(s=>s.Id==id);
            return View(size);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int id,Models.Size size)
        {
            Models.Size existedSize = await _context.Sizes.FirstOrDefaultAsync(s=>s.Id==id);
            if(existedSize == null) return NotFound();
            if (id != size.Id) return BadRequest();
            existedSize.Name = size.Name;
            //_context.Sizes.Update(size);
            await _context.SaveChangesAsync();
               
            return RedirectToAction("Index");
        }

         public async Task<IActionResult> Delete(int id)
         {
            Models.Size size = await _context.Sizes.FirstOrDefaultAsync(s => s.Id == id);
            if (size == null) return NotFound();
            return View(size);
          
         }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteSize(int id)
        {
            Models.Size size = await _context.Sizes.FirstOrDefaultAsync(s=>s.Id == id);
            if(size == null) return NotFound(); 
                                                                                    
            
            _context.Sizes.Remove(size);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
