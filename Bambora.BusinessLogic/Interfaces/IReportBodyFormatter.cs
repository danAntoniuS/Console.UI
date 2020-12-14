using Bambora.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bambora.BusinessLogic
{
    public interface IReportBodyFormatter
    {
        string GetReportBody(Dictionary<DateTime, Price> bodyInfo);
    }
}
