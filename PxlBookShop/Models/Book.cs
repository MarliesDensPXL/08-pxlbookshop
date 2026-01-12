using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PxlBookShop.Models
{
    public class Book
    {
        public Book(string title, string author, decimal price)
        {
            Title = title;
            Author = author;
            Price = price;
        }

        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }

    }
}
