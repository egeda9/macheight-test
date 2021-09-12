using macheight.nba.model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace macheight.nba.service
{
    /// <summary>
    /// public interface ICalculator
    /// </summary>
    public interface ICalculator
    {
        Task<IList<PlayerMatch>> GetMatchAsync(int input);
    }
}
