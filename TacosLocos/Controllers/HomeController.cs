using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TacosLocos.Models;

namespace TacosLocos.Controllers
{
    public class HomeController : Controller
    {
        public Data data;
        public DeliveryManager deliveryManager = new DeliveryManager();

        public ActionResult Index()
        {
            deliveryManager.LoadDeliveries();
            return View();
        }

        public ActionResult Deliveries()
        {
            ViewBag.Message = "Your Taco Deliveries Listed Below!";
            ViewBag.Deliveries = deliveryManager.ReturnDeliveries();

            return View();
        }

        [HttpPost]
        public ActionResult Deliveries(Order order, string action)
        {

            switch (action)
            {
                case "delete":
                    deliveryManager.Delete(order);
                    ViewBag.Message = "Delivery for " + order.Name + " has been deleted";
                    break;
                case "save":
                    deliveryManager.UpdateDelivere(order);
                    ViewBag.Message = "Delivery for " + order.Name + " has been updated";
                    break;
            }

            ModelState.Clear();
            ViewBag.Deliveries = deliveryManager.ReturnDeliveries();

            return View();
        }

        public ActionResult AddNew()
        {
            ViewBag.Message = "Create a new Taco Delivery Below!";

            return View();
        }

        [HttpPost]
        public ActionResult AddNew(Order order)
        {
            deliveryManager.AddDelivery(order);

            ViewBag.Message = "New delivery succesfully added";
            ModelState.Clear();

            return View();
        }

    }
}