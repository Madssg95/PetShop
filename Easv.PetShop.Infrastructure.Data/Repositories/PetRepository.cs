using Easv.PetShop.Core.Domain_Service;
using Easv.PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easv.PetShop.Infrastructure.Data.Repositories
{
    public class PetRepository : IPetRepository
    {

        public Pet CreatePet(Pet pet)
        {
            pet.Id = FakeDB.PetId++;
            var pets = FakeDB.ListOfPets.ToList();
            pets.Add(pet);
            FakeDB.ListOfPets = pets;
            return pet;
        }

        public Pet DeletePet(int id)
        {
            var pets = FakeDB.ListOfPets.ToList();
            var petToDelete = pets.FirstOrDefault(pet => pet.Id == id);
            pets.Remove(petToDelete);
            FakeDB.ListOfPets = pets;
            return petToDelete;
        }

        public Pet ReadByID(int id)
        {
            var pets = FakeDB.ListOfPets.ToList();
            var matchingPet = pets.FirstOrDefault(pet => pet.Id == id);
            return matchingPet;
        }

        public IEnumerable<Pet> ReadPets()
        {
            return FakeDB.ListOfPets;
        }

        public Pet UpdatePet(Pet updatePet)
        {
            var pets = ReadPets();
            var pet = pets.FirstOrDefault(pet1 => pet1.Id == updatePet.Id);

            pet.Name = updatePet.Name;
            pet.Type = updatePet.Type;
            pet.Birthday = updatePet.Birthday;
            pet.SoldDate = updatePet.SoldDate;
            pet.Color = updatePet.Color;
            pet.PreviousOwner = updatePet.PreviousOwner;
            pet.Price = updatePet.Price;

            FakeDB.ListOfPets = pets;
            return updatePet;
        }
    }
}
