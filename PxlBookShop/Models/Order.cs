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

		public string Email
		{
			get { return _email; }
			set { _email = value; }
		}


		public string StudentNumber
		{
			get { return _number; }
			set { _number = value; }
		}

		public void AddBook(Book book)
		{
			if (_books.Any(b => b.Title == book.Title)) 
			{
				throw new ArgumentException("Dit boek zit al in het winkelmandje.");
			}

			_books.Add(book);
		}

		public decimal CalculateTotalAmount()
		{
			decimal total = 0;
			foreach (Book book in _books)
			{
				total += book.Price;
			}
			decimal discount = total * 0.10;
			return discount;
		}
    }
}
