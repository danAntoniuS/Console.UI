using Bambora.Common;
using Bambora.Models;
using System;
using System.Threading.Tasks;

namespace Bambora.Repositories
{
    public class StockPriceRepository:IGetStockPriceHistory
    {
        private readonly IClientProxy clientProxy;
        private const string queryString = @"query?function=TIME_SERIES_DAILY&symbol={0}&outputsize=full&apikey=A3U8E3F7N3A85K86";

        public StockPriceRepository(IClientProxy clientProxy)
        {
            this.clientProxy = clientProxy;
        }

        public async Task<StockTimeSeriesDaily> GetStockPriceHistory(string symbol)
        {
            string url = string.Format(queryString, symbol);
            var stockSeries = await clientProxy.GetAsync<StockTimeSeriesDaily>(url)
                                               .ConfigureAwait(continueOnCapturedContext: false);
            return stockSeries;
        }
    }
}
