using System.Collections.Generic;
namespace samuraiApp.Domain
{
    public class Samurai
    {
        public int id {get; set;}
        public string Name {get; set;}
        public List<Quote> Quotes {get; set;} = new List<Quote>();
    }
}