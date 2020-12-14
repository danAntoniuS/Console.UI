using Bambora.Models;
using Bambora.Repositories;
using System;
using System.Threading.Tasks;

namespace Bambora.BusinessLogic
{
    public class Stock: ISaveStockHistory
    {
        IGetStockPriceHistory repoStockPrice;
        IReportHeaderFormatter headerFormatter;
        IReportBodyFormatter bodyFormatter;

        public Stock(IGetStockPriceHistory repoStockPrice, IReportHeaderFormatter headerFormatter, IReportBodyFormatter bodyFormatter)
        {
            this.repoStockPrice = repoStockPrice;
            this.headerFormatter = headerFormatter;
            this.bodyFormatter = bodyFormatter;
        }

        public async Task SaveStockHistory(string symbol, string path)
        {
            var stockInfo = await repoStockPrice.GetStockPriceHistory(symbol)
                                                .ConfigureAwait(continueOnCapturedContext: false);
            var header = headerFormatter.GetReportHeader(stockInfo.MetaData);
            var body = bodyFormatter.GetReportBody(stockInfo.TimeSeriesDaily);
            string report = $"{ header} {body}";
            //could have abstracted the file save too by using an interface && class
            System.IO.File.WriteAllLines(path, new string[] { report });
        }
    }
}
