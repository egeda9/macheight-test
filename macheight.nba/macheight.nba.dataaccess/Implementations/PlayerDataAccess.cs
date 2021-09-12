using macheight.nba.model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace macheight.nba.dataaccess.Implementations
{
    /// <summary>
    /// public class PlayerDataAccess
    /// </summary>
    public class PlayerDataAccess : IPlayerDataAccess
    {
        private readonly IConfigurationRoot _configuration;        
        private string _baseUrl;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public PlayerDataAccess(IConfigurationRoot configuration)
        {
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            GetConfigInfo();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<Players> Get()
        {
            try
            {
                using var client = new HttpClient();
                var response = await client.GetAsync(this._baseUrl);

                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Players>(responseBody);

                return result;
            }
            catch (Exception ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }

        private void GetConfigInfo()
        {
            this._baseUrl = _configuration.GetSection("DataAccess").GetSection("Url").Value;
        }
    }
}
