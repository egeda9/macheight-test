using macheight.nba.model;
using macheight.nba.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace macheight.nba
{
    /// <summary>
    /// public class Launcher
    /// </summary>
    public class Launcher
    {
        private readonly ICalculator _calculator;

        /// <summary>
        /// public Launcher(ICalculator calculator)
        /// </summary>
        /// <param name="calculator"></param>
        public Launcher(ICalculator calculator)
        {
            this._calculator = calculator ?? throw new ArgumentNullException(nameof(calculator));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task Run(int input)
        {
            try
            {
                var playersMatch = await this._calculator.GetMatchAsync(input);
                Printer(playersMatch);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Printer(IList<PlayerMatch> playersMatch)
        {
            if (!playersMatch.Any())
            {
                Console.WriteLine("No matches found");
            }
            else
            {
                foreach(var item in playersMatch)
                {
                    foreach(var player1 in item.Players1)
                    {
                        foreach (var player2 in item.Players2)
                        {
                            Console.WriteLine($"- {player1.FirstName} {player1.LastName}        {player2.FirstName} {player2.LastName}");
                            Console.WriteLine();
                        }
                    }
                }                
            }

            Console.ReadKey();
        }
    }
}
