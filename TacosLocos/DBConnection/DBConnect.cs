using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TacosLocos.Models;

namespace TacosLocos.DBConnection
{
    public static class DBConnect
    {
        private static readonly string DBTest = @"DBConnection\Data\OrdersTest.json";
        private static readonly string DataLink = "DBConnection\\Data\\Orders.json";


        public static string GetPathToDB()
        {
            return System.Web.HttpContext.Current.Request.PhysicalApplicationPath + DataLink;
        }

        public static string GetPathToDBTest()
        {
            return ReturnPathFolder(DBTest, 2);
        }

        public static List<Order> GetDeliveries(string filePath = null)
        {
            try
            {
                if (String.IsNullOrEmpty(filePath))
                {
                    filePath = GetPathToDB();
                }

                Data jsonResponse = JsonConvert.DeserializeObject<Data>(GetJSON(filePath));
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
                    filePath = GetPathToDB();
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

                if (count != deliveries.Count)
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

        public static string ReturnPathFolder(string dirName, int dirsBack = 3)
        {
            string properPath = "";
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string[] dirs = path.Split('\\');
            int num = dirs.Length - dirsBack;

            for (int i = 0; i < num; i++)
            {
                properPath = properPath + dirs[i] + "\\";
            }

            return properPath + dirName;
        }
    }
}