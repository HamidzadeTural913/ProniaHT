using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia_HT.DAL;
using Pronia_HT.Models;
using Pronia_HT.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pronia_HT.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public async Task<IActionResult> Index()
        {
            HomeVM model = new HomeVM()
            {
                Sliders = await _context.Sliders.OrderBy(s=>s.Order).ToListAsync(),
                Cards = await _context.Cards.ToListAsync(),
                Plants = await _context.Plants.Include(p=>p.PlantImages).ToListAsync(),
                Settings = await _context.Settings.FirstOrDefaultAsync()
                
            };
            
            return View(model);

        }
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
       
    }
}
