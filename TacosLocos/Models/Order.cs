using System;
using System.Linq;
using System.Text;

namespace TacosLocos.Models
{
    public class Order
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public bool Display { get; set; }

        public string SerialID { get; set; }

        public Order(string Name, string Address, bool Display, string SerialID)
        {
            this.Name = Name;
            this.Address = Address;
            this.Display = Display;
            this.SerialID = SerialID;
        }

        public Order(string Name, string Address, bool Display)
        {
            this.Name = Name;
            this.Address = Address;
            this.Display = Display;
            this.SerialID = RandomHash();
        }

        public Order(string Name, string Address)
        {
            this.Name = Name;
            this.Address = Address;
            this.Display = true;
            this.SerialID = RandomHash();
        }

        public Order()
        {
            Name = Name;
            Address = Address;
            Display = Display;
            SerialID = SerialID;
        }

        private string RandomHash()
        {
            StringBuilder builder = new StringBuilder();

            Enumerable
               .Range(65, 26)
                .Select(e => ((char)e).ToString())
                .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
                .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
                .OrderBy(e => Guid.NewGuid())
                .Take(11)
                .ToList().ForEach(e => builder.Append(e));
            
            return builder.ToString();
        }

        public void CreateHash()
        {
            this.SerialID = RandomHash();
        }
    }
}