using Easv.PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easv.PetShop.Infrastructure.Data
{
    public class FakeDB
    {
        public static int PetId = 1;
        public static IEnumerable<Pet> ListOfPets = new List<Pet>();
    }
}
