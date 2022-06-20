using System.Collections.Generic;

namespace Pronia_HT.Areas.ProniaAdmin.Controllers
{
    public class Setting
    {
        public int Id { get; set; }
        public string HeaderLogo { get; set; }
        public string FooterLogo { get; set; }
        public string SearchIcon { get; set; }
        public string AccountIcon { get; set; }
        public string WishlistIcon { get; set; }
        public string BasketIcon { get; set; }
        public string Phone { get; set; }
        public string AdvImage { get; set; }
        public List<SocialMedia> SocialMedias { get; set; }
    }
}
