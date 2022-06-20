﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia_HT.DAL;
using Pronia_HT.Extensions;
using Pronia_HT.Models;
using Pronia_HT.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Pronia_HT.Areas.ProniaAdmin.Controllers
{
    [Area("ProniaAdmin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
       public async Task<IActionResult> Index()
        {
            List<Slider>sliders = await _context.Sliders.ToListAsync();
            return View(sliders);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (!ModelState.IsValid) return View();
            if(slider.Photo != null)
            {
                //if (!slider.Photo.IsImage())
                //{
                //    ModelState.AddModelError("photo", "Sekil daxil edin");
                //    return View();
                //}
                //if (slider.Photo.IsGreater(2))
                //{
                //    ModelState.AddModelError("photo", "Duzgun olculu sekil daxil edin");
                //    return View();
                //}

                if (!slider.Photo.IsOkay(2))
                {
                    ModelState.AddModelError("photo", "Secdiyiniz sekilde problem var");
                    return View();
                }





                slider.Image = await slider.Photo.FileCreate(_env.WebRootPath,@"assets\images\website-images");
                await _context.Sliders.AddAsync(slider);    
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("photo", "sekil secin!");
                return View();
            }

        }

        public async Task<IActionResult> Edit(int id)
        {
            Models.Slider slider = await _context.Sliders.FirstOrDefaultAsync(s=>s.Id==id);
            if (slider==null) return BadRequest();
            return View(slider);

        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Edit(int id,Models.Slider slider)
        {
            Models.Slider existedSlider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if(existedSlider==null) return NotFound();
            if (id != slider.Id) return BadRequest();

            existedSlider.Title = slider.Title;
            existedSlider.Subtitle = slider.Subtitle;   
            existedSlider.Order = slider.Order;
            existedSlider.DiscoverUrl = slider.DiscoverUrl; 
            existedSlider.Discount = slider.Discount;   
            existedSlider.Image=slider.Image;
            

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            Models.Slider slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if(slider==null) return NotFound(); 
            return View(slider);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteSlider(int id)
        {
            Models.Slider slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if(slider==null) return NotFound();
            _context.Sliders.Remove(slider);
             await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
