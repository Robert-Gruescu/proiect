using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Newtonsoft.Json; // Adaugat pentru serializare JSON

namespace proiectul_1.Models
{
    // File: Library.cs
    public class Library
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Book> Books { get; set; }
        public List<User> Users { get; set; }

        public Library(string name, string address)
        {
            Name = name;
            Address = address;
            Books = new List<Book>();
            Users = new List<User>();
        }

        public void AddBook(Book book)
        {
            Books.Add(book);
            Console.WriteLine($"Cartea '{book.Title}' a fost adaugata.");
        }

        public void RemoveBook(string isbn)
        {
            var book = Books.Find(b => b.ISBN == isbn);
            if (book != null)
            {
                Books.Remove(book);
                Console.WriteLine($"Cartea '{book.Title}' a fost stearsa.");
            }
            else
            {
                Console.WriteLine("Cartea nu a fost gasita.");
            }
        }

        public void SaveToFile(string filePath)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Newtonsoft.Json.Formatting.Indented
            };

            string json = JsonConvert.SerializeObject(this, settings);
            File.WriteAllText(filePath, json);
            Console.WriteLine($"Datele bibliotecii au fost salvate in fisierul '{filePath}'.");
        }

        public static Library LoadFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                };

                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<Library>(json, settings);
            }
            Console.WriteLine($"Fisierul '{filePath}' nu exista.");
            return null;
        }

        public void ListAllBooks()
        {
            Console.WriteLine("=== Lista Cartilor ===");
            foreach (var book in Books)
            {
                Console.WriteLine(book);
            }
        }

        public void ListAllUsers()
        {
            Console.WriteLine("=== Lista Utilizatorilor ===");
            foreach (var user in Users)
            {
                Console.WriteLine(user);
            }
        }
    }
}