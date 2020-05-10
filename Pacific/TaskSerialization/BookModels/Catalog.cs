using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace TaskSerialization.BookModels
{
	[XmlRoot("catalog", Namespace = "http://library.by/catalog")]
	public class Catalog
	{
		public Catalog()
		{
			this.BookSurrogate = new Book[0];
		}

		[XmlIgnore]
		public DateTime Date { get; set; }
		[XmlAttribute("date")]
		public string DateString
		{
			get
			{
				return this.Date.ToString("yyyy-MM-dd");
			}
			set
			{
				this.Date = DateTime.Parse(value);
			}
		}

		[XmlIgnore]
		public IEnumerable<Book> Books { get; set; }

		[XmlElement("book"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public Book[] BookSurrogate 
		{
			get
			{
				return Books.ToArray();
			}
			set
			{
				Books = value;
			}
		}
	}
}
