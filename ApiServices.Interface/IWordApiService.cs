using IntegrationService.Models.Models;
using System.Threading.Tasks;

namespace ApiServices.Interface
{
    public interface IWordApiService
    {
        Task<ResponseModel> GetValidWord(string word);
    }
}
