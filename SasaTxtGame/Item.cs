using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace SasaTxtGame
{
    public class Item
    {
        public string Key { get; set; }
        public string Description { get; set; }
        public int Weight { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string StatusParam { get; set; }
        public string Command { get; set; }
        public bool CanPass { get; set; }
        public string Direction { get; set; }
        public Item subitem { get; set; }
    }
}
