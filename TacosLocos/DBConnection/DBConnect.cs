using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TacosLocos.Models;

namespace TacosLocos.DBConnection
{
    public class DBConnect
    {
        //private static string pathToDb = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"TacosLocos\DBConnection\Data\Orders.json"); 
        //private static string pathToDb = "C:\\Users\\cday\\Desktop\\Orders.json";
        private static string pathToDb = @"~\DBConnection\Data\Orders.json";

        public static List<Order> GetDeliveries(string filePath = null)
        {
            try
            {
                if (String.IsNullOrEmpty(filePath))
                {
                    filePath = pathToDb;
                }

                Data jsonResponse = JsonConvert.DeserializeObject<Data>(GetJSON(pathToDb));
                return jsonResponse.Deliveries;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void SaveDeliveries(List<Order> deliveries, string filePath = null)
        {

            try
            {
                if (String.IsNullOrEmpty(filePath))
                {
                    filePath = pathToDb;
                }

                using (StreamWriter file = File.CreateText(filePath))
                {

                        file.WriteLine(CreateJSONProper(deliveries));
                    
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static string CreateJSONProper(List<Order> deliveries)
        {
            int count = 0;
            string json = "{\"Deliveries\" : [";

            foreach (Order order in deliveries)
            {
                json = json + JsonConvert.SerializeObject(order);
                count++;

                if(count != deliveries.Count)
                {
                    json = json + ",";
                }
            }

            return json + "]}";
        }

        private static string GetJSON(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    using (StreamReader reader = new StreamReader(fileName))
                    {
                        string jsonText = reader.ReadToEnd();
                        reader.Close();
                        return jsonText;
                    }
                }
                else
                {
                    throw new FileNotFoundException("File path is incorrect");
                }
            }
            catch (IOException e)
            {
                throw new IOException("The file could not be read : " + e.Message);
            }
        }
    }
}