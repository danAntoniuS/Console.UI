using Bambora.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bambora.Repositories
{
    public interface IGetStockPriceHistory
    { 
        Task<StockTimeSeriesDaily> GetStockPriceHistory(string symbol);
    }
}
