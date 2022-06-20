 using Pronia_HT.Areas.ProniaAdmin.Controllers;
using Pronia_HT.Models;
using System.Collections.Generic;

namespace Pronia_HT.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public List<Card> Cards { get; set; }
        public Setting Settings { get; set; }
        public List<Plant> Plants { get; set; }
        

    }
}
