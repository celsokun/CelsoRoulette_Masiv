using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace CelsoRoulette_Masiv_Dto
{
    public class ResultRouletteCloseModel
    {
        public bool Status { get; set; }
        public string SaveMessage { get; set; }
        public RouletteModel Roulette { get; set; }
    }
}