using System;
namespace CelsoRoulette_Masiv_Dto
{
    public class RouletteModel
    {
        public Guid IdRoulette { get; set; }
        public bool Open { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
