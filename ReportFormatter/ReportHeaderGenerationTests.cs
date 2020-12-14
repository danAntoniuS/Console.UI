using Bambora.BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ReportFormatter
{
    [TestClass]
    public class ReportHeaderGenerationTests
    {
        [TestMethod]
        public void Report_Can_Generate_Header()
        {
            //Arrange
            string headerOutput1 = @"1. Information Daily Prices (open, high, low, close) and Volumes";
            string headerOutput2 = @"2. Symbol MSFT";
            string headerOutput3 = @"3. Last Refreshed 2020-12-11";
            string headerOutput4 = @"4. Output Size Full size";
            string headerOutput5 = @"5. Time Zone US/Eastern";

            var headerFormatter = new ReportHeaderFormatter();
            var metadata = new Dictionary<string, string>();
            metadata["1. Information"] = "Daily Prices (open, high, low, close) and Volumes";
            metadata["2. Symbol"] = "MSFT";
            metadata["3. Last Refreshed"] = "2020-12-11";
            metadata["4. Output Size"] = "Full size";
            metadata["5. Time Zone"] = "US/Eastern";

            //Act
            string header = headerFormatter.GetReportHeader(metadata);
            
            //Assert
            Assert.IsTrue(header.IndexOf(headerOutput1) >= 0);
            Assert.IsTrue(header.IndexOf(headerOutput2) >= 0);
            Assert.IsTrue(header.IndexOf(headerOutput3) >= 0);
            Assert.IsTrue(header.IndexOf(headerOutput4) >= 0);
            Assert.IsTrue(header.IndexOf(headerOutput5) >= 0);
        }
    }
}
