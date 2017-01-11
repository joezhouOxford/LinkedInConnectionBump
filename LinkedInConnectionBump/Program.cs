using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LinkedInConnectionBump
{

    public class Program
    {
        static int AddedConnections = Config.AddedConnections;
        static int AddConnectionLimit = Config.AddConnectionLimit;
        static int ConnectionBatchSize = Config.ConnectionBatchSize;
        static void Main(string[] args)
        {
            while (AddedConnections < AddConnectionLimit)
            {
                //ADUser ADUser =GetUserFromADByEmail( "joe.zhou@rbs.com");
                using (var ngDriver = new ChromeDriver())
                {
                    ngDriver.Manage().Timeouts().SetScriptTimeout(new TimeSpan(9999999999));
                    LogIn(ngDriver);
                    var URL = "https://www.linkedin.com/people/pymk?trk=nav_responsive_sub_nav_pymk";
                    OpenURL(ngDriver, URL);
                    AddConnection(ngDriver);
                }

                Main(args);
            }

            return;



        }

        private static void RunJavaScript(ChromeDriver ngDriver, string JS)
        {
            IJavaScriptExecutor js = ngDriver as IJavaScriptExecutor;
            var result = js.ExecuteScript(@"var connections=$('button.bt-request-buffed:contains(""Connect"")');connections.click();return connections.length");

        }



        private static void AddConnection(ChromeDriver ngDriver)
        {
            var JS = @"var batchLimit = " + ConnectionBatchSize + ";";
            JS = JS + ReadJavaScript("AddConnections.js");
            var result = RunJavaScriptAsync(ngDriver, JS);
            int numberOfInvite = int.Parse(result);
            AddedConnections += numberOfInvite;
            Config.AddUpdateAppSettings("AddedConnections", AddedConnections.ToString());

            WaitUntilFinish(ngDriver);


        }

        public static string ReadJavaScript(string path)
        {

            var JS = File.ReadAllText(path);
            return JS.Replace("\r\n", String.Empty);
        }

        private static void WaitUntilFinish(ChromeDriver ngDriver)
        {
            var foundResult = ngDriver.FindElements(By.ClassName("bt-request-buffed"));

            if (foundResult.Count > 0)
            {

                RunJavaScriptAsync(ngDriver, @"var callback = arguments[0];var connections=$('button.bt-request-buffed:contains(""Connect"")'); connections.click();setTimeout(function(){ callback(); }, 5000); ");
                WaitUntilFinish(ngDriver);
            }
        }


        private static string RunJavaScriptAsync(ChromeDriver ngDriver, string JS)
        {
            IJavaScriptExecutor js = ngDriver as IJavaScriptExecutor;
            return (string)js.ExecuteAsyncScript(JS);
        }
        private static void OpenURL(ChromeDriver ngDriver, string URL)
        {


            ngDriver.Url = URL;



        }

        private static void LogIn(ChromeDriver ngDriver)
        {
            ngDriver.Url = "https://www.linkedin.com";
            var username = ngDriver.FindElement(By.ClassName("login-email"));
            username.SendKeys(Config.LinkedInUserName);
            var password = ngDriver.FindElement(By.ClassName("login-password"));
            password.SendKeys(Config.LinkedInPassword);
            password.Submit();
        }




    }
}
