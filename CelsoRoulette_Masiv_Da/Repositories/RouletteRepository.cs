using CelsoRoulette_Masiv_Repository.Interfaces;
using CelsoRoulette_Masiv_Dto;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;
namespace CelsoRoulette_Masiv_Repository.Repositories
{
    public class RouletteRepository : IRouletteRepository
    {
        private readonly IDatabase db;
        public RouletteRepository(IDatabase IDatabase)
        {
            db = IDatabase;
        }
        public async Task<string> SaveOne()
        {
            RouletteModel RouletteModel = new RouletteModel();
            RouletteModel.IdRoulette = Guid.NewGuid();
            RouletteModel.Open = true;
            RouletteModel.CreationDate = DateTime.UtcNow;
            RouletteModel.ModificationDate = RouletteModel.CreationDate;
            bool Save = await db.HashSetAsync(ConfigConst.RouletteColeccion(), RouletteModel.IdRoulette.ToString(), JsonConvert.SerializeObject(RouletteModel));
            return Save ? RouletteModel.IdRoulette.ToString() : null;
        }
    }
}
