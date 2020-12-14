using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bambora.Common
{ 
    public interface IClientProxy
    {
        Task<T> GetAsync<T>(string url);
    }
}
