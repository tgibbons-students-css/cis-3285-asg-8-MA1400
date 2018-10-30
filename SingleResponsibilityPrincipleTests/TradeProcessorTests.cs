using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingleResponsibilityPrinciple;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple.Tests
{
    [TestClass()]
    public class TradeProcessorTests
    {
        private int CountDbRecords()
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\malwan1\source\repos\cis-3285-asg-8-MA1400\tradedatabase.mdf;Integrated Security=True;Connect Timeout=30"))
            {   if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                
                string myScalarQuery = "SELECT COUNT(*) FROM trade";
                SqlCommand myCommand = new SqlCommand(myScalarQuery, connection);
               // myCommand.Connection.Open();
                int count = (int)myCommand.ExecuteScalar();
                connection.Close();
                return count;
            }
        }
       
       
        [TestMethod()]
        public void TestNormalFile()
        {
            //Arrange
            var tradeStram = Assembly.GetExecutingAssembly().GetManifestResourceStream("SingleResponsibilityPrincipleTests.goodtrades.txt");
            var TradeProcessor = new TradeProcessor();

            //Act
            int countBefore = CountDbRecords();
            TradeProcessor.ProcessTrades(tradeStram);

            //Assert
            int countAfter = CountDbRecords();
            Assert.AreEqual(countBefore + 4, countAfter);
        }
        
        [TestMethod()]
    public void TestBadFile()
    {
        //Arrange
        var tradeStram = Assembly.GetExecutingAssembly().GetManifestResourceStream("SingleResponsibilityPrincipleTests.badtrades.txt");
        var TradeProcessor = new TradeProcessor();

        //Act
        int countBefore = CountDbRecords();
        TradeProcessor.ProcessTrades(tradeStram);

        //Assert
        int countAfter = CountDbRecords();
        Assert.AreEqual(countBefore, countAfter);
      }
        [TestMethod()]
        public void TestNegative()
        {
            //Arrange
            var tradeStram = Assembly.GetExecutingAssembly().GetManifestResourceStream("SingleResponsibilityPrincipleTests.negativetrades.txt");
            var TradeProcessor = new TradeProcessor();
            //Act

            int countBefore = CountDbRecords();
            TradeProcessor.ProcessTrades(tradeStram);

            //Assert
            int countAfter = CountDbRecords();
            Assert.AreEqual(countBefore, countAfter);
        }
    }

}