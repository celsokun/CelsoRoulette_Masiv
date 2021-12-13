using CelsoRoulette_Masiv_Dto;
using CelsoRoulette_Masiv_Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace CelsoRoulette_Masiv_Api.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class RouletteController : ControllerBase
    {
        readonly IRouletteRepository _IRouletteRepository;
        public RouletteController(IRouletteRepository IRouletteRepository)
        {
            _IRouletteRepository = IRouletteRepository;
        }
        [HttpGet]
        public ActionResult Get()
        {
            return Ok();
        }
        [HttpPost]
        [Route("SaveOne")]
        public async Task<ActionResult<string>> SaveOne()
        {
            try
            {
                string idResult = await _IRouletteRepository.SaveOne();
                if (idResult != null)
                {
                    return Ok(idResult);
                }
                else
                {
                    return StatusCode(500, ConfigConst.ERRORCREATEROULETTE);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpPost]
        [Route("OpenOne")]
        public async Task<ActionResult<ResultModel>> OpenOne(Guid rouletteId)
        {
            try
            {
                ResultModel ResultModel = await _IRouletteRepository.OpenOne(rouletteId);
                return Ok(ResultModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpPost]
        [Route("NewBet")]
        public async Task<ActionResult<ResultModel>> NewBet(BetModel NewBet)
        {
            try
            {
                Request.Headers.TryGetValue("UserId", out var userId);
                NewBet.UserId = userId;
                if (NewBet.BetColor != null) { NewBet.BetColor = NewBet.BetColor.ToUpper(); }
                ResultModel ResultModel = await _IRouletteRepository.NewBet(NewBet);
                return Ok(ResultModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpGet]
        [Route("GetAllRoulette")]
        public async Task<ActionResult<string>> GetAllRoulette()
        {
            try
            {
                return Ok(await _IRouletteRepository.GetAllRoulette());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
