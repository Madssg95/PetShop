using Easv.PetShop.Core.Entity;
using System;
using System.Collections.Generic;

namespace Easv.PetShop.Core.Application_Service.Service
{
    public interface IPetService
    {

        //Create
        Pet AddPet(Pet pet);
        Pet NewPet(string name, string type, DateTime birthdate, DateTime soldDate, string color, string previousOwner, double price);

        //Read
        List<Pet> GetPets();
        Pet GetPetById(int id);
        List<Pet> SearchByType(string type);
        List<Pet> Get5CheapestPets();
        List<Pet> SortByPrice();

        //Update
        Pet UpdatePet(Pet pet);

        //Delete
        Pet DeletePet(int id);
    }
}
