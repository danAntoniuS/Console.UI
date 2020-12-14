using Bambora.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bambora.BusinessLogic
{
    public interface ISaveStockHistory
    {
        Task SaveStockHistory(string symbol, string path);
    }
}
