using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PxlBookShop.Models
{
    public class Order
    {
		private List<Book> _books;
		private string _email;
		private int _number;

        public List<Book> Books { get; set; }

        public string Email
		{
			get { return _email; }
			set { _email = value; }
		}


		public int StudentNumber
		{
			get { return _number; }
			set { _number = value; }
		}

		public Order()
		{
			Books = new List<Book>();
		}

		public void AddBook(Book book)
		{			
			if (Books.Any(b => b.Title == book.Title)) 
			{
				throw new ArgumentException("Dit boek zit al in het winkelmandje.");
			}

			Books.Add(book);
		}

		public decimal CalculateTotalAmount()
		{
			decimal total = 0;
			foreach (Book book in Books)
			{
				total += book.Price;
			}
			decimal discount = total * 0.10m;
			return total - discount;
		}
    }
}
