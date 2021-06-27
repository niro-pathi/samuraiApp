using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using samuraiApp.Data;
using samuraiApp.Domain;

namespace samuraiApp.UI
{
    class Program
    {
        private static SamuraiContext _context = new SamuraiContext();

        static void Main(string[] args)
        {
            _context.Database.EnsureCreated();
           // AddSamuraisByName("Shimada","Okamoto","Kikuchio","Hayashida");
           // AddVariousTypes();
            RetriveAndUpdateMultipleSamurais();
            GetSamurais("");

            Console.WriteLine("Press any key to continue..!");
            Console.ReadKey();
        }

        private static void RetriveAndUpdateMultipleSamurais()
        {
            var samurais = _context.Samurais.Skip(1).Take(4).ToList();
            samurais.ForEach(s => s.Name += "San ");
            _context.SaveChanges();
        }

        private static void AddVariousTypes()
        {
            _context.AddRange(new Samurai { Name = "Shimada" },
                               new Samurai { Name = "Okamoto" },
                               new Battle { Name = "Battle of Anegawa" },
                               new Battle { Name = "Battle of Nagashino" });
            _context.SaveChanges();
        }

        public static void AddSamuraisByName(params string[] names)
        {
            foreach (string name in names)
            {
                _context.Samurais.Add(new Samurai { Name = name });
            }
            _context.SaveChanges();
        }

        private static void GetSamurais(string text)
        {
            var samurais = _context.Samurais
                .TagWith("ConsoleApp.Program.GetSamurais method")
                .ToList();
            Console.WriteLine($"{text} : Samurai count is {samurais.Count}");

            foreach (var samurai in samurais)
            {

                Console.WriteLine(samurai.Name);
            }
        }
    }
}
