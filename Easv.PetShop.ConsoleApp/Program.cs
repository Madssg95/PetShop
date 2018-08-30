using Easv.PetShop.Core.Application_Service;
using Easv.PetShop.Core.Application_Service.Service;
using Easv.PetShop.Core.Domain_Service;
using Easv.PetShop.Infrastructure.Data;
using Easv.PetShop.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Easv.PetShop.ConsoleApp
{
    public class Program
    {
        private static IPetRepository petRepository = new PetRepository();
        private static IPetService petService = new PetService(petRepository);

        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IPetRepository, PetRepository>() ;
            serviceCollection.AddScoped<IPetService, PetService >();
            serviceCollection.AddScoped<IPrinter, Printer>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var Printer = serviceProvider.GetRequiredService<IPrinter>();

            Printer.StartUI();

            Console.ReadLine();
        }
    }
}
