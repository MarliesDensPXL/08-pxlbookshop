using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PxlBookShop.Models
{
    public class Order
    {
		private List<Book> _books = new List<Book>(); //TODO: remove init
		private string _email;

		public string Email
		{
			get { return _email; }
			set { _email = value; }
		}

		private string _studentNumber;

		public string StudentNumber
		{
			get { return _studentNumber; }
			set { _studentNumber = value; }
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
			decimal sum = 0;
			foreach (Book book in _books)
			{
				sum += book.Price;
			}
			return sum;
		}
    }
}
