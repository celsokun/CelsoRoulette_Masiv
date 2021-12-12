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
                    return StatusCode(500, ConfigConst.ERRORCREATEREOULETTE);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
