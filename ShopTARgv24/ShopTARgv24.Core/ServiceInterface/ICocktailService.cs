using System.Threading.Tasks;
using ShopTARgv24.Core.Dto;

namespace ShopTARgv24.Core.ServiceInterface
{
    public interface ICocktailService
    {
        Task<DrinkResponseDto?> SearchByName(string name);
        Task<DrinkResponseDto?> LookupById(string id);    
    }
}
