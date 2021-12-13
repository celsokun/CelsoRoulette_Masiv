using System;
using System.ComponentModel.DataAnnotations;
namespace CelsoRoulette_Masiv_Dto
{
    public class BetModel
    {
        public Guid IdBet { get; set; }
        [Required]
        public Guid IdRoulette { get; set; }
        public string UserId { get; set; }
        [Range(minimum: 0, maximum: 36, ErrorMessage = ConfigConst.ERRORBETNUMBER)]
        public short? BetNumber { get; set; }
        public string BetColor { get; set; }
        [Range(minimum: 1, maximum: 10000, ErrorMessage = ConfigConst.ERRORBETVALUE)]
        public double BetValue { get; set; }

    }
}
