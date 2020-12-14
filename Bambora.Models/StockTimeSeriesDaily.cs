using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Bambora.Models
{
    public class StockTimeSeriesDaily
    {
        [JsonPropertyName("Meta Data")]
        public Dictionary<string, string> MetaData { get; set; }

        [JsonPropertyName("Time Series (Daily)")]
        public Dictionary<DateTime, Price> TimeSeriesDaily { get; set; }
    }

    public class Price
    {
        [JsonPropertyName("2. high")]
        public string High { get; set; }

        [JsonPropertyName("3. low")]
        public string Low { get; set; }
    }
}



