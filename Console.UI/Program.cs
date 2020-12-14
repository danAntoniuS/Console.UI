using Bambora.Common;
using Bambora.Repositories;
using Bambora.BusinessLogic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace Console.UI
{
    class Program
    {
        static Task Main(string[] args)
        {
            if(args == null || args[0] == null)
            {
                throw new ArgumentNullException("Please pass a stock as a first argument");
            }

            using IHost host = CreateHostBuilder(args).Build();           
            var task = IGetStock(host, args[0]);
            try
            {
                task.Wait();
            }
            catch(Exception ex)
            {
                string message = ex.Message;
            }
            return host.RunAsync();
        }

        static async Task IGetStock(IHost host, string symbol)
        {
            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;

            var repo = provider.GetRequiredService<IGetStockPriceHistory>();
            var reportHeaderFormatter = provider.GetRequiredService<IReportHeaderFormatter>();
            var reportBodyFormatter = provider.GetRequiredService<IReportBodyFormatter>();
            ISaveStockHistory stockHistory = new Stock(repo, reportHeaderFormatter, reportBodyFormatter);
            string file = ConfigurationManager.AppSettings["file"];
            await stockHistory.SaveStockHistory(symbol, file).ConfigureAwait(continueOnCapturedContext: false);
        }
        static IHostBuilder CreateHostBuilder(string[] args)
        {
           return Host.CreateDefaultBuilder(args)
                    .ConfigureServices((_, services) =>
                    {
                        services.AddSingleton((service) =>
                        {
                            string url = ConfigurationManager.AppSettings["url"];
                            int? timeout = Convert.ToInt32(ConfigurationManager.AppSettings["timeout"]);

                            if (string.IsNullOrWhiteSpace(url))
                            {
                                throw new ArgumentNullException("Missing Service Url");
                            }

                            var client = new HttpClient();
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            client.Timeout = TimeSpan.FromSeconds(timeout ?? 300);
                            client.BaseAddress = new Uri(url);
                            return client;
                        });

                        services.AddTransient<IClientProxy, ClientProxy>();
                        services.AddTransient<IGetStockPriceHistory, StockPriceRepository>(); 
                        services.AddTransient<ISaveStockHistory, Stock>(); 
                        services.AddTransient<IReportHeaderFormatter, ReportHeaderFormatter>();  
                        services.AddTransient<IReportBodyFormatter, ReportBodyFormatter>();
                    });

        }
    }
}
