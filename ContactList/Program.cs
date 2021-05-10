// Brian Hodge
// C00170400
// CMPS 358
// Project #7

using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ContactList
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new ContactDbContext();
            
            // ShowContacts(db);
            // AddAContact(db, "Betty", "333");
            // ShowContacts(db);
            // DeleteAContact(db, "Ralph");
            // ShowContacts(db);
            // DeleteAContact(db, "Betty");
            // ShowContacts(db);
            // DeleteAllContacts(db);
            // ShowContacts(db);
            // ShowContactsWithId(db);
            
            Console.WriteLine(1 + " - Show Contacts");
            Console.WriteLine(2 + " - Add Contact");
            Console.WriteLine(3 + " - Delete Contact by Name");
            Console.WriteLine(4 + " - Delete Contact by Id");
            Console.WriteLine(5 + " - Delete All Contacts");
            Console.WriteLine(0 + " - Exit");
            var userInput = Convert.ToInt32(Console.ReadLine());            
            while (userInput != 0)
            {

                if (userInput == 1)
                {
                    ShowContacts(db);
                    Console.WriteLine();
                }
                else if (userInput == 2)
                {
                    Console.Write("Name: ");
                    var name = Console.ReadLine();
                    Console.Write("Phone: ");
                    var phone = Console.ReadLine();
                    AddAContact(db, name, phone);
                    Console.WriteLine();
                }
                else if (userInput == 3)
                {
                    Console.Write("Name: ");
                    var name = Console.ReadLine();
                    DeleteAContact(db, name);
                    Console.WriteLine();
                }
                else if (userInput == 4)
                {
                    ShowContactsWithId(db);
                    Console.Write("Id: ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    DeleteContactById(db, id);
                    Console.WriteLine();
                }
                else if (userInput == 5)
                {
                    DeleteAllContacts(db);
                    Console.WriteLine();
                }
                Console.WriteLine(1 + " - Show Contacts");
                Console.WriteLine(2 + " - Add Contact");
                Console.WriteLine(3 + " - Delete Contact by Name");
                Console.WriteLine(4 + " - Delete Contact by Id");
                Console.WriteLine(5 + " - Delete All Contacts");
                Console.WriteLine(0 + " - Exit");
                userInput = Convert.ToInt32(Console.ReadLine());


            }

        }

        static void ShowContacts(ContactDbContext db)
        {
            var contactList = db
                .Contacts
                .OrderBy(x => x.Name);
            
            foreach(var c in contactList)
                Console.WriteLine(c.Name + ", " + c.Phone);
            Console.WriteLine();
        }

        static void AddAContact(ContactDbContext db, String name, String phone)
        {
            try
            {
                db.Contacts.Add(new Contact {Name = name, Phone = phone});
                db.SaveChanges();
            }
            catch
            {
                Console.WriteLine($"Adding {name} failed\n");
            }
        }

        static void DeleteAContact(ContactDbContext db, string name)
        {
            var thingie = db.Contacts.FirstOrDefault(_ => _.Name == name);
            try
            {
                if (thingie != null)
                {
                    db.Entry(thingie).State = EntityState.Modified;
                    db.Contacts.Remove(thingie);
                    db.SaveChanges();
                }
            }
            catch
            {
                Console.WriteLine($"{name} cannot be deleted\n");
            }
        }

        // 2(a): static method to delete all contacts from the database
        static void DeleteAllContacts(ContactDbContext db)  
        {
            var delete =
                from x in db.Contacts
                where x != null
                select x;

            foreach (var e in delete)
            {
                db.Contacts.Remove(e);
                db.SaveChanges();
            }
        }

        // 2(b): static method to find and list all contacts and their info, including Id values
        static void ShowContactsWithId(ContactDbContext db)
        {
            var contact = db
                .Contacts
                .OrderBy(x => x.Id);

            foreach (var c in contact)
            {
                Console.WriteLine(c.Id + ", " + c.Name + ", " + c.Phone);
            }
            Console.WriteLine();
            
        }

        // 2(c): static method to delete a contact from database based on it's Id
        static void DeleteContactById(ContactDbContext db, int id)
        {
            var toDelete = db.Contacts.FirstOrDefault(_ => _.Id == id);
            try
            {
                if (toDelete != null)
                {
                    db.Entry(toDelete).State = EntityState.Modified;
                    db.Contacts.Remove(toDelete);
                    db.SaveChanges();
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine($"{id} cannot be deleted\n");
                throw;
            }
        }
        
    }
}