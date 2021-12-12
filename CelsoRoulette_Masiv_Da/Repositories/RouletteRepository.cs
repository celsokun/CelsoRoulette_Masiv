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
            RouletteModel.Status = StatusRouletteModel.Created;
            RouletteModel.CreationDate = DateTime.UtcNow;
            RouletteModel.ModificationDate = RouletteModel.CreationDate;
            bool Save = await HashSetAsync(RouletteModel);
            return Save ? RouletteModel.IdRoulette.ToString() : null;
        }
        public async Task<ResultOpenModel> OpenOne(Guid IdRoulette)
        {
            ResultOpenModel ResultOpenModel = new ResultOpenModel();
            RedisValue Roulette = await db.HashGetAsync(ConfigConst.RouletteColeccion(), IdRoulette.ToString());
            if (!string.IsNullOrEmpty(Roulette))
            {
                RouletteModel RouletteModel = JsonConvert.DeserializeObject<RouletteModel>(Roulette);
                RouletteModel.Status = StatusRouletteModel.Open;
                await HashSetAsync(RouletteModel);
                RedisValue Roulette_ = await db.HashGetAsync(ConfigConst.RouletteColeccion(), IdRoulette.ToString());
                if (RouletteModel.Status == JsonConvert.DeserializeObject<RouletteModel>(Roulette_).Status)
                {
                    ResultOpenModel.Status = true;
                    ResultOpenModel.SaveMessage = ConfigConst.OPENROULETTE;
                }
                else
                {
                    ResultOpenModel.Status = false;
                    ResultOpenModel.SaveMessage = ConfigConst.ERROROPENROULETTE;
                }
                return ResultOpenModel;
            }
            ResultOpenModel.Status = false;
            ResultOpenModel.SaveMessage = ConfigConst.ERROROPENROULETTE;
            return ResultOpenModel;
        }
        private async Task<bool> HashSetAsync(RouletteModel RouletteModel)
        {
            return await db.HashSetAsync(ConfigConst.RouletteColeccion(), RouletteModel.IdRoulette.ToString(), JsonConvert.SerializeObject(RouletteModel));
        }
    }
}
