using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia_HT.DAL;
using Pronia_HT.Extensions;
using Pronia_HT.Models;
using Pronia_HT.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pronia_HT.Areas.ProniaAdmin.Controllers
{
    [Area("ProniaAdmin")]
    public class PlantController : Controller
    {




        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public PlantController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Plant> plants = await _context.Plants.Include(p => p.PlantImages).ToListAsync();
            return View(plants);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Sizes = await _context.Sizes.ToListAsync();
            ViewBag.Colors = await _context.Colors.ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Create(Plant plant)
        {
            ViewBag.Sizes = await _context.Sizes.ToListAsync();
            ViewBag.Colors = await _context.Colors.ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();

            if (!ModelState.IsValid) return View();
            if (plant.MainImage == null || plant.AnotherImage == null)
            {
                ModelState.AddModelError("", "Please choose main image or another image");
                return View();
            }
            if (!plant.MainImage.IsOkay(2))
            {
                ModelState.AddModelError("", "Please choose 2 mb under photo");
                return View();
            }


            foreach (var image in plant.AnotherImage)
            {
                if (!image.IsOkay(2))
                {
                    ModelState.AddModelError("AnotherImage", "Please choose 2 mb under photo");
                    return View();
                }
            }

            plant.PlantImages = new List<PlantImage>();



            PlantImage mainImage = new PlantImage
            {
                ImagePath = await plant.MainImage.FileCreate(_env.WebRootPath, @"assets\images\website-images"),
                IsMain = true,
                Plants = plant
            };

            plant.PlantImages.Add(mainImage);

            foreach (var image in plant.AnotherImage)
            {
                PlantImage another = new PlantImage
                {
                    ImagePath = await image.FileCreate(_env.WebRootPath, @"assets\images\website-images"),
                    IsMain = false,
                    Plants = plant
                };

                plant.PlantImages.Add(another);
            }

            await _context.Plants.AddAsync(plant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Sizes = await _context.Sizes.ToListAsync();
            ViewBag.Colors = await _context.Colors.ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();
            Plant plant = await _context.Plants.Include(p => p.PlantImages).FirstOrDefaultAsync(p => p.Id == id);
            if (plant == null) return NotFound();
            return View(plant);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Edit(int id, Plant plant)
        {
            ViewBag.Sizes = await _context.Sizes.ToListAsync();
            ViewBag.Colors = await _context.Colors.ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();

            Plant existed = await _context.Plants.Include(p => p.PlantImages).FirstOrDefaultAsync(p => p.Id == id);
            if (existed == null) return NotFound();

            if (plant.ImageIds == null && plant.AnotherImage == null)
            {
                ModelState.AddModelError("", "Butun sekilleri silemmersen!");
                return View(existed);
            }
            List<PlantImage> removableImages = existed.PlantImages.Where(p => p.IsMain == false && !plant.ImageIds.Contains(p.Id)).ToList();
            existed.PlantImages.RemoveAll(p => removableImages.Any(ri => ri.Id == p.Id));

            foreach (var image in removableImages)
            {
                FileUtilities.FileDelete(_env.WebRootPath, @"assets\images\website-images", image.ImagePath);
            }

            foreach (var image in plant.AnotherImage)
            {
                PlantImage plantImage = new PlantImage
                {
                    ImagePath= await image.FileCreate(_env.WebRootPath,@"assets\images\website-images"),
                    IsMain=false,
                    PlantId =existed.Id
                };
                existed.PlantImages.Add(plantImage);
            }

            _context.Entry(existed).CurrentValues.SetValues(plant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }
    }
}
