using MauiApp2.Models;
using System.Reflection;
using System.Text.Json;

namespace MauiApp2.ViewModels
{
    internal static class DummyDataProvider
    {
        public static List<Team> GetTeams()
        {
            var assembly = typeof(DummyDataProvider).GetTypeInfo().Assembly;

            using var stream = assembly.GetManifestResourceStream("Teams.json");
            using var reader = new StreamReader("Teams.json");
            var json = reader.ReadToEnd();

            return JsonSerializer.Deserialize<List<Team>>(json);
        }

        public static List<Product> GetProducts()
        {
           List<Product> products = new List<Product>
           {
               new Product {Name="Product 1"},
               new Product {Name="Product 2"},
               new Product {Name="Product 3"},
               new Product {Name="Product 4"},
               new Product {Name="Product 5"},
               new Product {Name="Product 6"},
               new Product {Name="Product 7"},
               new Product {Name="Product 8"},
               new Product {Name="Product 9"},
               new Product {Name="Product 10"},
               new Product {Name="Product 11"},
               new Product {Name="Product 12"},
               new Product {Name="Product 13"},
           };
            return products;
        }

        public static List<Event> GetEvents()
        {
            List<Event> events = new List<Event>
           {
               new Event {Name="Event 1"},
               new Event {Name="Event 2"},
               new Event {Name="Event 3"},
               new Event {Name="Event 4"},
               new Event {Name="Event 5"},
               new Event {Name="Event 6"},
               new Event {Name="Event 7"},
               new Event {Name="Event 8"},
               new Event {Name="Event 9"}             

           };
            return events;
        }

        public static List<Card> GetCards()
        {
            List<Card> events = new()
            {
               new Card {Name="Card 1"},
               new Card {Name="Card 2"},
               new Card {Name="Card 3"},
               new Card {Name="Card 4"},
               new Card {Name="Card 5"},
               new Card {Name="Card 6"},
           };
            return events;
        }
    }
}
