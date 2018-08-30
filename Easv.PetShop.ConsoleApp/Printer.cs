using Easv.PetShop.Core.Application_Service.Service;
using Easv.PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easv.PetShop.ConsoleApp
{
    class Printer : IPrinter
    {
        private IPetService _petService;

        

        public Printer(IPetService petService)
        {
            _petService = petService;
        }

        public void StartUI()
        {
            #region TestData
            //CreatePet("Dingo", "Dog", new DateTime(2000, 10, 10), new DateTime(2010, 10, 10), "White", "Hans", 500.5);
            //CreatePet("Buller", "Dog", new DateTime(2005, 03, 07), new DateTime(2011, 10, 10), "Black", "Martin", 1500.7);
            //CreatePet("Wuff", "Dog", new DateTime(2006, 05, 05), new DateTime(2015, 10, 10), "Grey", "Lukas", 800.5);
            //CreatePet("Karl", "Monkey", new DateTime(2003, 01, 01), new DateTime(2017, 10, 10), "Brown", "Mads", 2500.5);
            //CreatePet("Louis", "Monkey", new DateTime(2010, 11, 09), new DateTime(2018, 10, 10), "Red", "Lars", 3700.5);
            //CreatePet("Luffe", "Dolphin", new DateTime(2001, 12, 10), new DateTime(2007, 10, 10), "Grey", "Ole", 7500.5);
            #endregion

            string[] menu = new string[]
        {
            "Show list of all pets",
            "Search by type",
            "Create new pet",
            "Delete a pet",
            "Update a pet",
            "Sort pets by price",
            "Get 5 cheapest available pets",
            "Exit menu"
        };


            Console.WriteLine("Please choose want you want to do: \n");
            DisplayMenu(menu);
            var userChoice = GetUserInputInt("\nYour choice: ", 1, 8);

            while (userChoice != 8)
            {
                switch (userChoice)
                {
                    case 1:
                        PrintPets(_petService.GetPets());
                        break;
                    case 2:
                        var petsType = SearchByType(GetUserInput("Please choose a type: "));
                        PrintPets(petsType);
                        break;
                    case 3:
                        var name = GetUserInput("Name:");
                        var type = GetUserInput("Type:");
                        var birthday = GetUserInputDate("Birthday: (Format yyyy, mm, dd)");
                        var soldDate = GetUserInputDate("Sold date: (Format yyyy, mm, dd)");
                        var color = GetUserInput("Color:");
                        var prevOwner = GetUserInput("Previous owner:");
                        var price = GetUserInputDouble("Price:");
                        CreatePet(name, type, birthday, soldDate, color, prevOwner, price);
                        break;
                    case 4:
                        DeletePet(GetIdBorders("Id:"));
                        break;
                    case 5: 
                        var updateId = GetIdBorders("Id:");
                        updatePet(updateId);           
                        break;
                    case 6:
                        Console.WriteLine("Sorted list of pets by price: \n");
                        PrintPets(SortByPrice());
                        break;
                    case 7:
                        Console.WriteLine("List of the 5 cheapest pets: \n");
                        PrintPets(Get5CheapestPets());
                        break;
                    default:
                        break;
                }
                Console.WriteLine("Please choose what you want to do next! \n");
                DisplayMenu(menu);
                userChoice = GetUserInputInt("\nYour choice:", 1, 8);
            }
            Console.WriteLine("\nSee you next time!");
        }

        void PrintPets(List<Pet> pets)
        {
            if (pets.Count != 0)
            {
                foreach (var pet in pets)
                {
                    Console.WriteLine($"Id: {pet.Id}    Name: {pet.Name}    Type: {pet.Type}    Birthday: {pet.Birthday}    SoldDate: {pet.SoldDate}    Color: {pet.Color}    Previous owner: {pet.PreviousOwner}   Price: {pet.Price} \n");
                }
            }
            else
            {
                Console.WriteLine("The list of pets is empty. \n");
            }
            
        }

        List<Pet> SearchByType(string type)
        {
            var foundPets = _petService.SearchByType(type.ToLower());
            if (foundPets.Count == 0)
            {
                Console.WriteLine("\n We couldn't find any pets matching your type. \n");
            }
            return foundPets;
        }

        void CreatePet(string name, string type, DateTime birthday, DateTime soldDate, string color, string previousOwner, double price)
        {
            var petToCreate = _petService.NewPet(name, type, birthday, soldDate, color, previousOwner, price);
            if (_petService.AddPet(petToCreate) != null)
            {
                Console.WriteLine("The pet was succesfully created. \n");
            }
        }

        void DeletePet(int id)
        {
            if (_petService.DeletePet(id) == null)
            {
                Console.WriteLine("You have chosen a non-existing Id.\n");
            }
            else
            {
                Console.WriteLine("The pet was succesfully deleted. \n");
            }
        }

        private void updatePet(int idFound)
        {
            if (idFound != 0)
            {
                var updateName = GetUserInput("Name:");
                var updateType = GetUserInput("Type:");
                var updateBirthday = GetUserInputDate("Birthday: (Format yyyy, mm, dd)");
                var updateSoldDate = GetUserInputDate("Sold date: (Format yyyy, mm, dd)");
                var updateColor = GetUserInput("Color:");
                var updatePrevOwner = GetUserInput("Previous owner:");
                var updatePrice = GetUserInputDouble("Price:");
                var updatedPet = _petService.NewPet(updateName, updateType, updateBirthday, updateSoldDate, updateColor, updatePrevOwner, updatePrice);
                updatedPet.Id = idFound;
                if (_petService.UpdatePet(updatedPet) != null)
                {
                    Console.WriteLine("The pet has succesfully been updated. \n");
                }
            }
        }

        List<Pet> SortByPrice()
        {
            var sortedByPrice = _petService.SortByPrice();
            return sortedByPrice;
        }

        List<Pet> Get5CheapestPets()
        {
            var cheapest5Pets = _petService.Get5CheapestPets();
            return cheapest5Pets;
        }

        int GetIdBorders(string text)
        {
            var pets = _petService.GetPets();
            if (pets.Count == 0)
            {
                Console.WriteLine("The list of pets is empty.\n");
                return 0;

            }
            var lowestPetId = pets.FirstOrDefault().Id;
            var highestPetId = pets.OrderByDescending(pet => pet.Id).FirstOrDefault().Id;
            var selection = GetUserInputInt(text, lowestPetId, highestPetId);

            return selection;
        }

        void DisplayMenu(string[] menu)
        {
            for (int i = 0; i < menu.Length; i++)
            {
                Console.WriteLine($"{i + 1}: {menu[i]}");
            }
        }

        DateTime GetUserInputDate(string toInput)
        {
            DateTime date;
            Console.WriteLine(toInput);
            while (!DateTime.TryParse(Console.ReadLine(), out date))
            {
                Console.WriteLine("Please type in a valid date (Format yyyy, mm, dd)!");
            }
            return date;
        }

        string GetUserInput(string toInput)
        {
            Console.WriteLine(toInput);
            var input = Console.ReadLine();
            while (input.Length == 0)
            {
                Console.WriteLine("Please enter at least one character!");
                Console.WriteLine(toInput);
                input = Console.ReadLine();
            }
            return input;
        }

        double GetUserInputDouble(string toInput)
        {
            double selection;
            Console.WriteLine(toInput);
            while (!double.TryParse(Console.ReadLine(), out selection))
            {
                Console.WriteLine("Please enter a number!");
            }
            return selection;
        }

        int GetUserInputInt(string toInput, int lowerBorder, int upperBorder)
        {
            int selection;
            Console.WriteLine(toInput);
            while (!int.TryParse(Console.ReadLine(), out selection) || selection < lowerBorder || selection > upperBorder)
            {
                Console.WriteLine($"\nPlease enter a number between {lowerBorder} and {upperBorder}!\n");
                Console.WriteLine(toInput);
            }
            return selection;
        }
    }
}
