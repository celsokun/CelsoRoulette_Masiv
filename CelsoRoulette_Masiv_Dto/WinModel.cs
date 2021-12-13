using System;
using System.ComponentModel.DataAnnotations;
namespace CelsoRoulette_Masiv_Dto
{
    public class WinModel
    {
        public Guid IdBet { get; set; }
        public double PrizeValue { get; set; }
    }
}
