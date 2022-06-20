using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pronia_HT.Models
{
    public class Size
    {
        public int Id { get; set; }

        [StringLength(maximumLength:10,ErrorMessage="Please write 10 atribute")]
        public string Name { get; set; }
        public List<Plant> Plants { get; set; }

       
    }
}
