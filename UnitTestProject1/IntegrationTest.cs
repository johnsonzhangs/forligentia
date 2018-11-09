using Demo.Wage.API;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class IntegrationTest
    {
        TestServer _testServer;

        public IntegrationTest()
        {
            _testServer = new TestServer(WebHost
                   .CreateDefaultBuilder()
                   .UseStartup<Demo.Wage.API.Startup>()
                   .UseEnvironment("Development")
                   )
                   ;
        }


        [TestMethod]
        public void InputForAandBCase()
        {
            var client = _testServer.CreateClient();

            //Case : 
            //      1> Set grossWage to number 1000, 
            //      2> Set areaKey to an exist area "AreaA"
            //Expected: 
            //      1> API return Success, 
            //      2> Returned DeductedWage should equal 850 
            double grossWage = 1000;
            string areaKey = "AreaA";
            string url = string.Format("wage/DeductedTax/{0}/{1}", grossWage, areaKey);
            string result = client.GetStringAsync(url).Result;

            Assert.IsNotNull(result);

            ApiReturn rtn = JsonConvert.DeserializeObject<ApiReturn>(result);
            Assert.IsNotNull(rtn);
            Assert.AreEqual(ApiReturnCodes.Success, rtn.Code);

            Dictionary<string, object> data = JsonConvert.DeserializeObject<Dictionary<string, object>>(rtn.Data.ToString());

            Assert.AreNotSame(grossWage, data["DeductedWage"]);
            Assert.AreEqual((double)850, data["DeductedWage"]);



            //Case : 
            //      1> Set grossWage to number 1000, 
            //      2> Set areaKey to an exist area "AreaB"
            //Expected: 
            //      1> API return Success, 
            //      2> Return DeductedWage should equal 998 (GrossWage - 2)
            //      3> Return DeductedWage should not be same with last case returned DeductedWage
            string areaKey2 = "AreaB";
            string url2 = string.Format("wage/DeductedTax/{0}/{1}", grossWage, areaKey2);
            string result2 = client.GetStringAsync(url2).Result;

            Assert.IsNotNull(result2);

            ApiReturn rtn2 = JsonConvert.DeserializeObject<ApiReturn>(result2);

            Assert.IsNotNull(rtn2);
            Assert.AreEqual(ApiReturnCodes.Success, rtn2.Code);

            Dictionary<string, object> data2 = JsonConvert.DeserializeObject<Dictionary<string, object>>(rtn2.Data.ToString());

            Assert.AreNotSame(grossWage, data2["DeductedWage"]);
            Assert.AreEqual((double)998, data2["DeductedWage"]);

            Assert.AreNotEqual(data["DeductedWage"], data2["DeductedWage"]);
        }


        [TestMethod]
        public void NoAreaKeyInputCase()
        {
            var client = _testServer.CreateClient();

            //Case : 
            //      1> Set grossWage to number 1000, 
            //      2> No areaKey 
            //Expected: 
            //      1> API return AreaKeyNotExist, 

            double grossWage = 1000;
            string url = string.Format("wage/DeductedTax/{0}/", grossWage);
            string result = client.GetStringAsync(url).Result;

            Assert.IsNotNull(result);

            ApiReturn rtn = JsonConvert.DeserializeObject<ApiReturn>(result);

            Assert.IsNotNull(rtn);
            Assert.AreEqual(ApiReturnCodes.AreaKeyNotExist, rtn.Code);

        }

        [TestMethod]
        public void NoAreaKeyInputUsingLatestCase()
        {
            var client = _testServer.CreateClient();

            double grossWage = 1000;
            string areaKey = "AreaA";
            string url = string.Format("wage/DeductedTax/{0}/{1}", grossWage, areaKey);
            string result = client.GetStringAsync(url).Result;

            Assert.IsNotNull(result);

            ApiReturn rtn = JsonConvert.DeserializeObject<ApiReturn>(result);

            Assert.IsNotNull(rtn);
            Assert.AreEqual(ApiReturnCodes.Success, rtn.Code);



            url = string.Format("wage/DeductedTax/{0}", grossWage);
            string result2 = client.GetStringAsync(url).Result;
            Assert.IsNotNull(result);

            ApiReturn rtn2 = JsonConvert.DeserializeObject<ApiReturn>(result2);

            Assert.IsNotNull(rtn2);
            Assert.AreEqual(ApiReturnCodes.Success, rtn2.Code);
        }

        [TestMethod]
        public void AreaKeyNotExistCase()
        {

            //Case : 
            //      1> Set grossWage to number 1000, 
            //      2> Set areaKey to unknown area "NotExistKey"
            //Expected: 
            //      1> API return AreaKeyNotExist, 
            var client = _testServer.CreateClient();

            double grossWage = 1000;
            string areaKey = "NotExistKey";
            string url = string.Format("wage/DeductedTax/{0}/{1}", grossWage, areaKey);
            string result = client.GetStringAsync(url).Result;

            Assert.IsNotNull(result);

            ApiReturn rtn = JsonConvert.DeserializeObject<ApiReturn>(result);

            Assert.IsNotNull(rtn);
            Assert.AreEqual(ApiReturnCodes.AreaKeyNotExist, rtn.Code);

        }

        [TestMethod]
        public void GrossWageErrorCase()
        {

            var client = _testServer.CreateClient();


            //Case : 
            //      1> Set grossWage to a negative number -1000, 
            //      2> Set areaKey to an exist area "AreaA"
            //Expected: 
            //      1> API return GrossWageError, 

            double grossWage = -1000;
            string areaKey = "AreaA";
            string url = string.Format("wage/DeductedTax/{0}/{1}", grossWage, areaKey);
            string result = client.GetStringAsync(url).Result;

            Assert.IsNotNull(result);

            ApiReturn rtn = JsonConvert.DeserializeObject<ApiReturn>(result);

            Assert.IsNotNull(rtn);
            Assert.AreEqual(ApiReturnCodes.GrossWageError, rtn.Code);


            //Case : 
            //      1> Set grossWage to a string value "AAA", 
            //      2> Set areaKey to an exist area "AreaA"
            //Expected: 
            //      1> API return GrossWageError, 
            url = string.Format("wage/DeductedTax/ABC/{1}", grossWage, areaKey);
            string result2 = client.GetStringAsync(url).Result;

            Assert.IsNotNull(result2);

            ApiReturn rtn2 = JsonConvert.DeserializeObject<ApiReturn>(result2);

            Assert.IsNotNull(rtn2);
            Assert.AreEqual(ApiReturnCodes.GrossWageError, rtn2.Code);

        }


        [TestMethod]
        public void CalculationErrorCase()
        {

            var client = _testServer.CreateClient();


            //Case : 
            //      1> Set grossWage to number 1000, 
            //      2> Set areaKey to an exist area "AreaC"
            //Expected: 
            //      1> API return CalculationError, 

            double grossWage = 1000;
            string areaKey = "AreaC";
            string url = string.Format("wage/DeductedTax/{0}/{1}", grossWage, areaKey);
            string result = client.GetStringAsync(url).Result;

            Assert.IsNotNull(result);

            ApiReturn rtn = JsonConvert.DeserializeObject<ApiReturn>(result);

            Assert.IsNotNull(rtn);
            Assert.AreEqual(ApiReturnCodes.CalculationError, rtn.Code);

        }


        [TestMethod]
        public void AreaAWithCalBCase()
        {

            var client = _testServer.CreateClient();


            //Case : 
            //      1> Set grossWage to number 1000, 
            //      2> Set areaKey to an exist area "AreaAB"
            //Expected: 
            //      1> API return Success, 
            //      2> Returned DeductedWage should be 998 (using CalculationB to do)
            //      3> Returned AreaName should be "AreaA"(means class AreaA instance was loaded)

            double grossWage = 1000;
            string areaKey = "AreaAB";
            string url = string.Format("wage/DeductedTax/{0}/{1}", grossWage, areaKey);
            string result = client.GetStringAsync(url).Result;

            Assert.IsNotNull(result);

            ApiReturn rtn = JsonConvert.DeserializeObject<ApiReturn>(result);

            Assert.IsNotNull(rtn);
            Assert.AreEqual(ApiReturnCodes.Success, rtn.Code);

            Dictionary<string, object> data = JsonConvert.DeserializeObject<Dictionary<string, object>>(rtn.Data.ToString());

            Assert.AreNotSame(grossWage, data["DeductedWage"]);
            Assert.AreEqual((double)998, data["DeductedWage"]);
            Assert.AreEqual("Area A", data["AreaName"]);

        }
    }

}
