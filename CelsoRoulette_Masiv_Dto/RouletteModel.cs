using System;
using System.ComponentModel.DataAnnotations;

namespace CelsoRoulette_Masiv_Dto
{
    public class RouletteModel
    {
        [Required]
        public Guid IdRoulette { get; set; }
        [Required]
        public StatusRouletteModel Status { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
