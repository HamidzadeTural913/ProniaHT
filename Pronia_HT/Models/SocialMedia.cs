namespace Pronia_HT.Areas.ProniaAdmin.Controllers
{
    public class SocialMedia
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public int SettingId { get; set; }
        public Setting Settings { get; set; }
    }
}
