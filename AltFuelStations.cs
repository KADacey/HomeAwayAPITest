using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomeAwayAPITest
{
    [TestClass]
    public class AltFuelStations
    {
        private TestContext context;

        /// <summary>
        /// Gets or sets the test context which provides information about and functionality for the current test
        /// </summary>
        public TestContext TestContext
        {
            get { return context; }
            set { context = value; }
        }

        private static int stationId = 0;   //this will store the stationId between tests - bad practice!

        [TestMethod]
        [DeploymentItem("ValidateItemInList.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", ".\\ValidateItemInList.xml", "Row", DataAccessMethod.Sequential)]
        public void ValidateItemInList()
        {
            //read the data elements from the XML data file
            string baseURL = Convert.ToString(context.DataRow["baseURL"]);
            string dataFormat = Convert.ToString(context.DataRow["dataFormat"]);
            string APIKey = Convert.ToString(context.DataRow["APIKey"]);
            string locationName = Convert.ToString(context.DataRow["locationName"]);
            string evNetworkName = Convert.ToString(context.DataRow["evNetworkName"]);
            string expectedStationName = Convert.ToString(context.DataRow["expectedStationName"]);

            //build the full uri from the given components
            string uri = baseURL + dataFormat + "?api_key=" + APIKey + "&location=" + locationName + "&ev_network=" + evNetworkName;

            //query for the data and deserialize it
            var nearestAltFuelStation = DownloadSerializedJsonData<NearestAltFuelStation>(uri);

            //determine if the expected station name was returned
            if (!(nearestAltFuelStation.total_results > 0))
            {
                Assert.Fail("Station list not returned properly");
            }
            else
            {
                bool stationFound = false;

                // loop through results looking for the expected name
                for (int i = 0; i < nearestAltFuelStation.total_results; i++)
                {
                    if (String.Equals(nearestAltFuelStation.fuel_stations[i].station_name, expectedStationName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        //bad practice, but save the id for the next test case
                        stationId = Convert.ToInt32(nearestAltFuelStation.fuel_stations[i].id);
                        stationFound = true;
                        break;
                    }
                }
                Assert.IsTrue(stationFound, stationFound ? "The given station was found" : "The given station was not found:" + expectedStationName);
            }
        }

        [TestMethod]
        [DeploymentItem("ValidateStationAddress.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", ".\\ValidateStationAddress.xml", "Row", DataAccessMethod.Sequential)]
        public void ValidateStationAddress()
        {
            //read the data elements from the XML data file
            string baseURL = Convert.ToString(context.DataRow["baseURL"]);
            string dataFormat = Convert.ToString(context.DataRow["dataFormat"]);
            string APIKey = Convert.ToString(context.DataRow["APIKey"]);
            string expectedStationAddress = Convert.ToString(context.DataRow["expectedStationAddress"]);

            //build the full uri from the given components
            string uri = baseURL + stationId + dataFormat + "?api_key=" + APIKey;

            //query for the data and deserialize it
            var stationInfo = DownloadSerializedJsonData<StationById>(uri);

            //build the address string from the data returned
            string stationAddress = String.Format("{0}, {1}, {2}, {3}", stationInfo.alt_fuel_station.street_address, stationInfo.alt_fuel_station.city, stationInfo.alt_fuel_station.state, stationInfo.alt_fuel_station.zip);

            //determine if the data is as expected
            Assert.AreEqual(expectedStationAddress, stationAddress, true);
        }

        /// <summary>
        /// Makes a blocking call to download the data from the given uri then deserializes it to the specified class
        /// </summary>
        /// <typeparam name="T">Class representing the json data to be returned</typeparam>
        /// <param name="uri">The uri to query the json data from</param>
        /// <returns>The class populated with the returned data</returns>
        private static T DownloadSerializedJsonData<T>(string uri) where T : new()
        {
            using (var w = new WebClient())
            {
                var jsonData = string.Empty;

                // attempt to download JSON data as a string
                // this call blocks but will eventually timeout in case of an error
                try
                {
                    jsonData = w.DownloadString(uri);
                }
                catch (Exception e)
                {
                    Assert.Fail(e.Message);
                }

                // if string with JSON data is not empty, deserialize it to class and return its instance
                if (!String.IsNullOrEmpty(jsonData))
                {
                    return JsonConvert.DeserializeObject<T>(jsonData);
                }
                else
                {
                    Assert.Fail("No results returned from the query:" + uri);
                    return new T();
                }
            }
        }
    }
}
