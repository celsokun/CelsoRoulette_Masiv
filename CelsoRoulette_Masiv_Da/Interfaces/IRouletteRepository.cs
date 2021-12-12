using CelsoRoulette_Masiv_Dto;
using System.Threading.Tasks;
namespace CelsoRoulette_Masiv_Repository.Interfaces
{
    public interface IRouletteRepository
    {
        Task<string> SaveOne();
    }
}
