using System.Collections.Generic;
using TacosLocos.DBConnection;

namespace TacosLocos.Models
{
    public class DeliveryManager
    {
        private static List<Order> Deliveries { get; set; }

        public void LoadDeliveries(string filePath = null)
        {
            Deliveries = DBConnect.GetDeliveries(filePath);
        }

        public List<Order> ReturnDeliveries()
        {
            if (Deliveries != null)
            {
                return Deliveries;
            }
            else
            {
                LoadDeliveries();
                return Deliveries;
            }
        }

        public void AddDelivery(Order order)
        {
            if (order.SerialID != null)
            {
                Deliveries.Add(order);
                DBConnect.SaveDeliveries(Deliveries);
            }
            else
            {
                order.CreateHash();
                Deliveries.Add(order);
                DBConnect.SaveDeliveries(Deliveries);
            }
        }

        public void Delete(Order order)
        {
            Deliveries.RemoveAll(x => x.SerialID == order.SerialID);
            DBConnect.SaveDeliveries(Deliveries);
            LoadDeliveries();
        }

        public void UpdateDelivery(Order order)
        {
            Deliveries.RemoveAll(x => x.SerialID == order.SerialID);
            Deliveries.Add(order);
            DBConnect.SaveDeliveries(Deliveries);
            LoadDeliveries();
        }
    }
}