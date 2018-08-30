using System;
using System.Collections.Generic;
using System.Text;

namespace Easv.PetShop.Core.Entity
{
    public class Pet
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public DateTime Birthday { get; set; }

        public DateTime SoldDate { get; set; }

        public string Color { get; set; }

        public string PreviousOwner { get; set; }

        public double Price { get; set; }
    }
}
