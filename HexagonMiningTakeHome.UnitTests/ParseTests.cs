using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HexagonMiningTakeHome;
using System.Text.RegularExpressions;
namespace HexagonMiningTakeHome.UnitTests
{
    [TestClass]
    public class ParseTests
    {
        [TestMethod]
        public void Parse_CheckIfGet_True()
        {
            var parse = new Parse();
            string str = "\"GET";
            
            bool result = HexagonMiningTakeHome.Parse.CheckIfGet(str); ;

            Assert.IsTrue(result);
        }
        [TestMethod]
        public void Parse_CheckIfGet_False()
        {
            var parse = new Parse();
            string str = "\"POST";

            bool result = HexagonMiningTakeHome.Parse.CheckIfGet(str); ;

            Assert.IsFalse(result);
        }
        [TestMethod]
        public void Parse_CheckIfSuccess_True()
        {
            var parse = new Parse();
            string str = "200";

            bool result = HexagonMiningTakeHome.Parse.CheckIfSuccess(str);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void Parse_CheckIfSuccess_False()
        {
            var parse = new Parse();
            string str = "400";

            bool result = HexagonMiningTakeHome.Parse.CheckIfSuccess(str);

            Assert.IsFalse(result);
        }
        [TestMethod]
        public void Parse_CleanQuotes_False()
        {
            var parse = new Parse();
            string str = "\"Word\"";

            bool result = Regex.IsMatch(HexagonMiningTakeHome.Parse.CleanQuotes(str), "\"");

            Assert.IsFalse(result);
        }
        [TestMethod]
        public void Parse_CleanQuotes_True()
        {
            var parse = new Parse();
            string str = "\"Word\"";

            bool result = Regex.IsMatch(str, "\"");

            Assert.IsTrue(result);
        }
    }
}
