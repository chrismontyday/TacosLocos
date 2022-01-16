using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TacosLocos.DBConnection;

namespace TacosLocos.Models
{
    public class Data
    {
        public List<Order> Deliveries { get; set; }

    }
}