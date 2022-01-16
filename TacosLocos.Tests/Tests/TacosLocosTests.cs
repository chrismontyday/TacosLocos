using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using TacosLocos;
using TacosLocos.Controllers;
using TacosLocos.DBConnection;
using TacosLocos.Models;

namespace TacosLocos.Tests.Tests
{
    [TestClass]
    public class TacosLocosTests
    {
        private static string pathToDb = "C:\\Users\\cday\\Desktop\\OrdersTest.json";
        List<Order> deliveries = new List<Order>();
        DeliveryManager dm = new DeliveryManager();

        [TestInitialize]
        public void TestInitialize()
        {
            dm.LoadDeliveries();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (true)
            {
                string dumb = "boop";
            }
        }

        [TestMethod]
        public void IndexPageTest()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
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
            Assert.IsNotNull(DBConnect.GetDeliveries());
        }

        [TestMethod]
        public void SaveDeliveriesTest()
        {

            //deliveries.Add(new Order("TestName", "TestAddres", true));
            //DBConnect.SaveDeliveries(deliveries, pathToDb);

            Assert.IsNotNull(DBConnect.GetDeliveries(pathToDb));
        }

        [TestMethod]
        public void CreateJsonProperTest()
        {

            deliveries.Add(new Order("TestName", "TestAddress", true));
            deliveries.Add(new Order("TestName2", "TestAddress2", false));
            string json = DBConnect.CreateJSONProper(deliveries);

            Assert.AreEqual("{\"Deliveries\" : [{\"Name\":\"TestName\",\"Address\":\"TestAddress\",\"Display\":true},{\"Name\":\"TestName2\",\"Address\":\"TestAddress2\",\"Display\":false}]}", json);
        }
    }
}
