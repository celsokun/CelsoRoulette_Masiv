using CelsoRoulette_Masiv_Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace CelsoRoulette_Masiv_Repository.Interfaces
{
    public interface IRouletteRepository
    {
        Task<string> SaveOne();
        Task<ResultModel> OpenOne(Guid IdRoulette);
        Task<ResultModel> NewBet(BetModel BetModel);
        Task<List<RouletteModel>> GetAllRoulette();
    }
}
