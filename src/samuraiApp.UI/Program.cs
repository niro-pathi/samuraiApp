using System;
using System.Collections.Generic;
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
            //AddSamuraisByName("Shimada","Okamoto","Kikuchio","Hayashida");
            //AddVariousTypes();
            //RetriveAndUpdateMultipleSamurais();
            //InsertNewSamuraiWithAQuote();
            //InsertNewSamuraiWithMultipleQuotes();
            //AddQuoteToExistingSamuraiWhileTracked();
            //Simpler_AddQuoteToExistingSamuraiNotTracked(2);
            //EagerLoadSamuraiWithQuotes();
            FilteringWithRelatedData();

            //GetSamurais("");

            Console.WriteLine("Press any key to continue..!");
            Console.ReadKey();
        }

        private static void FilteringWithRelatedData()
        {
            var samurai = _context.Samurais
                                    .Where(s => s.Quotes.Any(q => q.Text.Contains("you")))
                                    .ToList();

            Console.WriteLine("\r");


            foreach (var item in samurai)
            {
                Console.WriteLine($" Samurai {item.Name}");

                foreach (var quote in item.Quotes)
                {
                    Console.WriteLine($"\t Quote {quote.Text}");
                }
            }
        }

        private static void EagerLoadSamuraiWithQuotes()
        {
            var result =
                _context.Samurais.Where(s => s.Name.Contains("San"))
                .Include(s => s.Quotes).FirstOrDefault();

            Console.WriteLine(result.Name);
            foreach (var item in result.Quotes)
            {
                Console.WriteLine( item.Text);

            }
            
        }

        private static void Simpler_AddQuoteToExistingSamuraiNotTracked(int samuraiId)
        {
            var quote = new Quote { Text = "Thanks for dinner!", SamuraiId = samuraiId};
            using var newContext = new SamuraiContext();
            newContext.Quotes.Add(quote);
            newContext.SaveChanges();
        }

        private static void AddQuoteToExistingSamuraiWhileTracked()
        {
            var samurai = _context.Samurais.FirstOrDefault();

            samurai.Quotes.Add(new Quote
            {
                Text = "I bet you're happy that I've saved you!"
            });

            _context.SaveChanges();
        }

        private static void InsertNewSamuraiWithMultipleQuotes()
        {
            var samurai = new Samurai
            {
                Name = "Kyuzo",
                Quotes = new List<Quote>
                {
                    new Quote {Text = "Watch out for my sharp sward!"},
                    new Quote {Text = "I told you to watch out for the sharp sward! oh Well!" }
                }
            };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }

        private static void InsertNewSamuraiWithAQuote()
        {
            var samurai = new Samurai
            {
                Name = "Kambei Shimada",
                Quotes = new List<Quote>
                {
                    new Quote {Text = "I've come to save you."}
                }
            };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }

        private static void RetriveAndUpdateMultipleSamurais()
        {
            var samurais = _context.Samurais.Skip(1).Take(4).ToList();
            samurais.ForEach(s => s.Name += " San");
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
