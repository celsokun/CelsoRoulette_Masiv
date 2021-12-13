using CelsoRoulette_Masiv_Repository.Interfaces;
using CelsoRoulette_Masiv_Dto;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

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
        public async Task<ResultModel> OpenOne(Guid IdRoulette)
        {
            ResultModel ResultModel = new ResultModel();
            RedisValue Roulette = await db.HashGetAsync(ConfigConst.RouletteColeccion(), IdRoulette.ToString());
            if (!string.IsNullOrEmpty(Roulette))
            {
                RouletteModel RouletteModel = JsonConvert.DeserializeObject<RouletteModel>(Roulette);
                RouletteModel.Status = StatusRouletteModel.Open;
                RouletteModel.ModificationDate = DateTime.UtcNow;
                await HashSetAsync(RouletteModel);
                RedisValue Roulette_ = await db.HashGetAsync(ConfigConst.RouletteColeccion(), IdRoulette.ToString());
                if (RouletteModel.Status == JsonConvert.DeserializeObject<RouletteModel>(Roulette_).Status)
                {
                    ResultModel.Status = true;
                    ResultModel.SaveMessage = ConfigConst.OPENROULETTE;
                }
                else
                {
                    ResultModel.Status = false;
                    ResultModel.SaveMessage = ConfigConst.ERROROPENROULETTE;
                }
                return ResultModel;
            }
            ResultModel.Status = false;
            ResultModel.SaveMessage = ConfigConst.ERROROPENROULETTE;
            return ResultModel;
        }
        public async Task<ResultModel> NewBet(BetModel BetModel)
        {
            ResultModel ResultModel = new ResultModel();
            RedisValue Roulette = await db.HashGetAsync(ConfigConst.RouletteColeccion(), BetModel.IdRoulette.ToString());
            if (!string.IsNullOrEmpty(Roulette))
            {
                RouletteModel RouletteModel = JsonConvert.DeserializeObject<RouletteModel>(Roulette);
                if (RouletteModel.Status != StatusRouletteModel.Open)
                {
                    ResultModel.Status = false;
                    ResultModel.SaveMessage = ConfigConst.ERRORCLOSEROULETTE;
                    return ResultModel;
                }
                if (RouletteModel.Bets == null) { RouletteModel.Bets = new List<BetModel>(); }
                ResultModel = ValidateBeat(BetModel);
                if (!ResultModel.Status) { return ResultModel; }
                RouletteModel.Bets.Add(BetModel);
                await HashSetAsync(RouletteModel);
                ResultModel.Status = true;
                ResultModel.SaveMessage = ConfigConst.BETSAVE;
                return ResultModel;
            }
            ResultModel.Status = false;
            ResultModel.SaveMessage = ConfigConst.ERRORNOTEXITSROULETTE;
            return ResultModel;
        }
        private async Task<bool> HashSetAsync(RouletteModel RouletteModel)
        {
            return await db.HashSetAsync(ConfigConst.RouletteColeccion(), RouletteModel.IdRoulette.ToString(), JsonConvert.SerializeObject(RouletteModel));
        }
        private ResultModel ValidateBeat(BetModel BetModel)
        {
            ResultModel ResultModel = new ResultModel();
            if (BetModel.BetNumber == null && BetModel.BetColor != "RED" && BetModel.BetColor != "BLACK")
            {
                ResultModel.Status = false;
                ResultModel.SaveMessage = ConfigConst.ERRORBETCOLORORNUMBER;
            }
            if (BetModel.BetNumber != null && BetModel.BetColor != null)
            {
                ResultModel.Status = false;
                ResultModel.SaveMessage = ConfigConst.ERRORBETCOLORANDNUMBER;
            }
            if (BetModel.UserId == null)
            {
                ResultModel.Status = false;
                ResultModel.SaveMessage = ConfigConst.ERRORUSERID;
            }
            ResultModel.Status = true;
            return ResultModel;
        }
        public async Task<List<RouletteModel>> GetAllRoulette()
        {
            List<RouletteModel> listRouletteDto = new List<RouletteModel>();
            var result = await db.HashGetAllAsync(ConfigConst.RouletteColeccion());
            foreach (var item in result)
            {
                listRouletteDto.Add(JsonConvert.DeserializeObject<RouletteModel>(item.Value));
            }
            return listRouletteDto;
        }
    }
}
