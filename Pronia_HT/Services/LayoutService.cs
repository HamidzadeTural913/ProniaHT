using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia_HT.Areas.ProniaAdmin.Controllers;
using Pronia_HT.DAL;
using System.Threading.Tasks;

namespace Pronia_HT.Services
{
    public class LayoutService
    {
        private readonly AppDbContext _context;

        public LayoutService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Setting> GetDatas()
        {
            return await _context.Settings.FirstOrDefaultAsync();
        }
    }
}
