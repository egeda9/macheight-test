using macheight.nba.dataaccess;
using macheight.nba.model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace macheight.nba.service.Implementation
{
    /// <summary>
    /// public class Calculator
    /// </summary>
    public class Calculator : ICalculator
    {
        private readonly IPlayerDataAccess _playerDataAccess;

        /// <summary>
        /// public Calculator(
        /// </summary>
        /// <param name="playerDataAccess"></param>
        public Calculator(IPlayerDataAccess playerDataAccess)
        {
            this._playerDataAccess = playerDataAccess ?? throw new ArgumentNullException(nameof(playerDataAccess));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IList<PlayerMatch>> GetMatchAsync(int input)
        {
            IList<PlayerMatch> result = new List<PlayerMatch>();

            try
            {
                var players = await this._playerDataAccess.Get();

                if (players?.Values != null && players.Values.Any())
                {
                    var playerHeights = (from p in players.Values
                                         group p by p.HeightInches into g
                                         select new PlayerHeight { Height = g.Key, Players = g.ToList() })
                                    .ToList();

                    result = GetPlayers(playerHeights, input);
                }                
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private IList<PlayerMatch> GetPlayers(IList<PlayerHeight> players, int input)
        {
            var result = new List<PlayerMatch>();

            for (var i = 0; i < players.Count; ++i)
            {
                if (int.TryParse(players[i].Height, out var value))
                {
                    var heightInches = value;
                    var temp = input - heightInches;

                    var matches = players.FirstOrDefault(p => Convert.ToInt32(p.Height) == temp);

                    if (matches != null)
                    {
                        var playerMatch = new PlayerMatch
                        {
                            Players1 = players[i].Players,
                            Players2 = matches.Players
                        };

                        result.Add(playerMatch);
                    }
                }
            }

            return result;
        }
    }
}
