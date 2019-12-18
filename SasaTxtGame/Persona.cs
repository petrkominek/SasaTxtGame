using System;
using System.Collections.Generic;
using System.Text;

namespace SasaTxtGame
{
    public class Persona
    {
        public Persona()
        {
            this.Ruksak = new List<Item>();
        }
        public int x { get; set; }
        public int y { get; set; }

        public int OldX { get; set; }
        public int OldY { get; set; }

        public string Name { get; set; }
        public List<Item> Ruksak { get; set; }

        public void AddToRuksak(Item value)
        {
            this.Ruksak.Add(value);
        }
    }
}
