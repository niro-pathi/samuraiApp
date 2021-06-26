using System;
using System.Linq;
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
            GetSamurais("Before Add :  ");
            AddSamurai();
            GetSamurais("After Add :  ");
            Console.WriteLine("Press any key to continue..!");
            Console.ReadKey();
        }

        public static void AddSamurai()
        {
            var samurai = new Samurai { Name = "John" };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }
        private static void GetSamurais(string text)
        {
            var samurais = _context.Samurais.ToList();
            Console.WriteLine($"{text} : Samurai count is {samurais.Count}");

            foreach (var samurai in samurais)
            {

                Console.WriteLine(samurai.Name);
            }
        }
    }
}
