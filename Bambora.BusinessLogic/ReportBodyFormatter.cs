using Bambora.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bambora.BusinessLogic
{
    public class ReportBodyFormatter : IReportBodyFormatter
    {
        public string GetReportBody(Dictionary<DateTime, Price> bodyInfo)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("                                  ");
            sb.AppendLine($"{"Date".PadRight(21)} {"High".PadRight(18)} {"Low".PadRight(15)}");
            sb.AppendLine("*****************************************************");
            foreach (var kv in bodyInfo)
            {
                sb.AppendLine($"{kv.Key.ToString("mm/dd/yyy").PadRight(17)} {kv.Value.High.PadRight(15)} {kv.Value.Low.PadRight(15)}");
            }
            sb.AppendLine("*****************************************************");
            return sb.ToString();
        }
    }
}
