using macheight.nba.model;
using System.Threading.Tasks;

namespace macheight.nba.dataaccess
{
    public interface IPlayerDataAccess
    {
        Task<Players> Get();
    }
}
