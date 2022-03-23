
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataService.Interface
{
    public interface IWordDataService
    {
        Task<IEnumerable<string>> GetAllWordsAsync();
    }
}
