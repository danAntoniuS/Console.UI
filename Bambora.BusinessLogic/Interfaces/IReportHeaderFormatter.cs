using System;
using System.Collections.Generic;
using System.Text;

namespace Bambora.BusinessLogic
{
    public interface IReportHeaderFormatter
    {
        string GetReportHeader(Dictionary<string, string> headerInfo);
    }
}
