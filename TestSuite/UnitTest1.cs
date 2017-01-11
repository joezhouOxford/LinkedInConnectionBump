using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinkedInConnectionBump;

namespace TestSuite
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ReadJavaScriptTest()
        {
            var text = Program.ReadJavaScript("AddConnections.js");
            Assert.IsTrue(text.Length > 0);
        }
        [TestMethod]
        public void AddUpdateAppSettingsTest() { 
            Config.AddUpdateAppSettings("")
        }
    }
}
