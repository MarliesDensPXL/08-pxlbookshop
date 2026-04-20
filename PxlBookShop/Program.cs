using PxlBookShop.Models;
using System.Collections.Generic;
using System.Xml.Linq;

namespace PxlBookShop
{
    internal class Program
    {
        //Lijst van departementen:
        static List<Department> _departments = new List<Department>()
        {
            new Department()
            {
                Name = "PXL Education",
                Courses = new List<Course>()
                {
                    new Course(11, "Bachelor Kleuteronderwijs"),
                    new Course(13, "Bachelor Lager Onderwijs"),
                    new Course(14, "Bachelor Secundair Onderwijs")
                }
            },
            new Department()
            {
                Name = "PXL Digital",
                Courses = new List<Course>()
                {
                    new Course(1, "Graduaat Programmeren"),
                    new Course(2, "Graduaat Digitale Vormgeving"),
                    new Course(3, "Bachelor Toegepaste Informatica")
                }
            },
            new Department()
            {
                Name = "PXL Business",
                Courses = new List<Course>()
                {
                    new Course(65, "Graduaat Sales Support"),
                    new Course(66, "Bachelor Bedrijfsmanagement"),
                    new Course(68, "Graduaat Winkelmanagement"),
                }
            },
            new Department()
            {
                Name = "PXL Healthcare",
                Courses = new List<Course>()
                {
                    new Course(42, "Bachelor Ergotherapie"),
                    new Course(43, "Bachelor Vroedkunde"),
                }
            }
        };

        //Lijst van beschikbare boeken per opleiding
        static Dictionary<int, List<Book>> _books = new Dictionary<int, List<Book>>()
        {
            // PXL Digital
            {
                1, // Graduaat Programmeren
                new List<Book>()
                {
                    new Book("C# Fundamentals", "John Sharp", 39.99m),
                    new Book("Object-Oriented Programming in .NET", "Emily Turner", 44.50m),
                    new Book("Introduction to Algorithms", "Thomas Green", 59.00m)
                }
            },
            {
                2, // Graduaat Digitale Vormgeving
                new List<Book>()
                {
                    new Book("Graphic Design Basics", "Laura White", 34.95m),
                    new Book("Adobe Creative Cloud Essentials", "Mark Johnson", 49.99m),
                    new Book("UX Design Principles", "Sophie Brown", 42.75m)
                }
            },
            {
                3, // Bachelor Toegepaste Informatica
                new List<Book>()
                {
                    new Book("Advanced C# Programming", "Michael Roberts", 54.90m),
                    new Book("Software Architecture Patterns", "Anna Keller", 58.00m),
                    new Book("Databases and SQL", "Robert Martin", 46.25m)
                }
            },

            // PXL Business
            {
                65, // Graduaat Sales Support
                new List<Book>()
                {
                    new Book("Introduction to Sales", "Peter Collins", 32.50m),
                    new Book("Customer Relationship Management", "Linda Harris", 41.80m),
                    new Book("Business Communication Skills", "David Wilson", 29.95m)
                }
            },
            {
                66, // Bachelor Bedrijfsmanagement
                new List<Book>()
                {
                    new Book("Principles of Management", "Henry Adams", 47.00m),
                    new Book("Financial Accounting", "Susan Miller", 55.60m),
                    new Book("Strategic Marketing", "Kevin Thompson", 48.90m)
                }
            },
            {
                68, // Graduaat Winkelmanagement
                new List<Book>()
                {
                    new Book("Retail Management Essentials", "Nancy Brooks", 38.40m),
                    new Book("Visual Merchandising", "Oliver Stone", 45.00m),
                    new Book("Stock and Inventory Control", "Rachel Evans", 36.75m)
                }
            }
        };

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Welkom in de PXL Standaard Student Shop");
            Console.WriteLine("Bestel hier je cursussen en handboeken en krijg een fikse korting!");
            Console.WriteLine();
            Console.WriteLine("Persoonlijke gegevens");
            Console.WriteLine("---------------------");

            int studentNumber;
            string studentNumberInput;

            do
            {
                Console.Write("Studentnummer: ");
                studentNumberInput = Console.ReadLine();   

            } while (!int.TryParse(studentNumberInput, out studentNumber) || (studentNumberInput.Length != 8));

            string email;
            do
            {
                Console.Write("E-mailadres: ");
                email = Console.ReadLine();
            } while (!email.EndsWith("@student.pxl.be")); 
            
            Console.WriteLine();
            Console.WriteLine("Druk op enter om verder te gaan...");
            Console.ReadLine();
            Console.Clear();

            Order order = new Order();
            order.Email = email;
            order.StudentNumber = studentNumber;

            Department selectedDepartment = SelectDepartment();

            Course selectedCourse = SelectCourse(selectedDepartment);

            List<Book> books = _books[selectedCourse.Id];

            Console.WriteLine($"Cursussen voor de opleiding {selectedCourse.Name}:\n");
            for (int b = 0; b < books.Count; b++)
            {
                Console.WriteLine($"\t- {books[b].ToString}");
            } 
            Console.WriteLine();
            Console.WriteLine("Druk op enter om deze boeken nu te bestellen\nof druk op een andere toets om te annuleren.");

            if (Console.ReadKey(true).Key == ConsoleKey.Enter)
            {

                foreach (Book book in books)
                {
                    order.AddBook(book);
                }

                Console.ForegroundColor= ConsoleColor.Green;
                Console.WriteLine("Je bestelling werd succesvol geregistreerd.");
                Console.WriteLine($"Er werd een bevestigingsmail verstuurd naar {order.Email}.");
                Console.WriteLine($"Gelieve het bedrag van {order.CalculateTotalAmount()} te betalen om de bestelling definitief te maken.");
                Console.ResetColor();
                Console.WriteLine();
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Je bestelling werd geannuleerd.");
                Console.ResetColor();
                Console.WriteLine();
            }


                Console.WriteLine("De applicatie wordt nu afgesloten...");
                Console.ReadLine();
        }
        

        private static Department SelectDepartment()
        {
            Console.WriteLine("Selecteer je departement:\n");

            for (int d = 0; d < _departments.Count; d++) 
            {
                Console.WriteLine($"{d+1, 2}: {_departments[d].Name}"); 
            }
            Console.WriteLine();

            int index = -1;
            do
            {
                Console.Write("Geef het nummer van je departement: ");
            } while(!int.TryParse(Console.ReadLine(), out index) || index > _departments.Count );
            Console.Clear();

            return _departments[index-1]; 
        }

        private static Course SelectCourse(Department department)
        {
            Console.Write("Departement: ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(department.Name);
            Console.ResetColor();
            Console.WriteLine("Selecteer je opleiding:\n");

            Console.WriteLine("  Id | Opleiding");
            Console.WriteLine($"---- + --------------------");
            foreach (Course course in department.Courses)
                //(Course course in _departments[_departments.IndexOf(department)].Courses) //[0] vervangen door _departments.IndexOf(department) anders toont applicatie altijd de cursussen uit department op positie 0
            {
                Console.WriteLine($"{course.Id,4} | {course.Name}");
            }
            Console.WriteLine(); 

            int id;
            
            do
            {
                Console.Write("Geef het id van je opleiding: ");
            } while (!int.TryParse(Console.ReadLine(), out id)); department.Courses.Find(c => c.Id != id);        //TODO uitzoeken hoe dit werkt. Geen idee hoe ik id uit de cursus moet halen om te vergelijken met ingave van de gebruiker.


            Console.Clear();

            return department.Courses.Find(c => c.Id == id);
        }
    }
}
