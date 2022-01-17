using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Web.Mvc;
using TacosLocos.Controllers;
using TacosLocos.DBConnection;
using TacosLocos.Models;

namespace TacosLocos.Tests.Tests
{
    [TestClass]
    public class TacosLocosTests
    {
        private static string pathToDb;
        List<Order> deliveries = new List<Order>();
        DeliveryManager dm = new DeliveryManager();

        [TestInitialize]
        public void TestInitialize()
        {
            pathToDb = DBConnect.GetPathToDBTest();
            dm.LoadDeliveries(pathToDb);
        }

        [TestMethod]
        public void DeliveriesPageTest()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Deliveries() as ViewResult;

            // Assert
            Assert.AreEqual("Your Taco Deliveries Listed Below!", result.ViewBag.Message);
        }

        [TestMethod]
        public void ContactPageTest()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.AddNew() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetDeliveriesDoesNotReturNullTest()
        {
            Assert.IsNotNull(DBConnect.GetDeliveries(pathToDb));
        }

        [TestMethod]
        public void SaveDeliveriesDoesNotReturnNullTest()
        {
            Assert.IsNotNull(DBConnect.GetDeliveries(pathToDb));
        }

        [TestMethod]
        public void SaveDeliveriesTest()
        {
            //Creating and adding order to Deliver
            Order order = new Order("TestName", "TestAddres", true);
            deliveries.Add(order);
            DBConnect.SaveDeliveries(deliveries, pathToDb);

            //Getting result
            List<Order> testOrder = DBConnect.GetDeliveries(pathToDb);
            Order orderTwo = testOrder[0];

            //Confirming results given and result recieved match
            Assert.AreEqual(orderTwo.SerialID, order.SerialID);
        }

        [TestMethod]
        public void CreateJsonProperTest()
        {
            string expectedResult = "{\"Deliveries\" : [{\"Name\":\"TestName\",\"Address\":\"TestAddress\",\"Display\":true,\"SerialID\":\"FakeID\"},{\"Name\":\"TestName2\",\"Address\":\"TestAddress2\",\"Display\":false,\"SerialID\":\"FakeID\"}]}";

            //Creating two generic Orders and confirming the JSON is generated properly.
            deliveries.Add(new Order("TestName", "TestAddress", true, "FakeID"));
            deliveries.Add(new Order("TestName2", "TestAddress2", false, "FakeID"));
            string actualJson = DBConnect.CreateJSONProper(deliveries);

            Assert.AreEqual(expectedResult, actualJson);
        }
    }
}
