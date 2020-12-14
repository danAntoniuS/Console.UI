using System;
using System.Collections.Generic;
using System.Text;

namespace Bambora.BusinessLogic
{
    public class ReportHeaderFormatter : IReportHeaderFormatter
    {
        public string GetReportHeader(Dictionary<string, string> headerInfo)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("*****************************************************");
            foreach (var kv in headerInfo)
            {
                sb.AppendLine($"{kv.Key} {kv.Value}");
            }
            sb.AppendLine("*****************************************************");
            return sb.ToString();
        }
    }
}
