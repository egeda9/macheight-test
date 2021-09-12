using macheight.nba.dataaccess;
using macheight.nba.dataaccess.Implementations;
using macheight.nba.service;
using macheight.nba.service.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;

namespace macheight.nba
{
    /// <summary>
    /// public class Program
    /// </summary>
    public class Program
    {
        public static IConfigurationRoot Configuration;

        public static int Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("Exception :: No arguments/invalid execution - At least should pass an integer input arg");
                return 1;
            }

            try
            {
                Console.WriteLine($"Process started :: {DateTime.Now}");
                MainAsync(args).Wait();
                Console.WriteLine($"Process successfully executed :: {DateTime.Now}");

                return 0;
            }
            catch
            {
                return 1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task MainAsync(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            try
            {
                if (int.TryParse(args[0], out var value))
                {
                    if (serviceProvider != null) 
                        await serviceProvider.GetService<Launcher>()?.Run(value);
                }
                else
                {
                    throw new InvalidDataException("Argument provided is not an integer value");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception :: {ex.Message}");
                throw;
            }
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            serviceCollection.AddSingleton(Configuration);

            serviceCollection.AddScoped<Launcher>();
            serviceCollection.AddScoped<IPlayerDataAccess, PlayerDataAccess>();
            serviceCollection.AddScoped<ICalculator, Calculator>();
        }
    }
}
