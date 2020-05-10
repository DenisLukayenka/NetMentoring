using System;
using System.Xml.Serialization;

namespace TaskSerialization.BookModels
{
	public class Book
	{
		[XmlAttribute("id")]
		public string Id { get; set; }

		[XmlElement("isbn")]
		public string IsBn { get; set; }

		[XmlElement("author")]
		public string Author { get; set; }
		[XmlElement("title")]
		public string Title { get; set; }
		[XmlElement("genre")]
		public string Genre { get; set; }
		[XmlElement("publisher")]
		public string Publisher { get; set; }
		[XmlElement("description")]
		public string Description { get; set; }

		[XmlIgnore]
		public DateTime RegistrationDate { get; set; }
		[XmlElement("registration_date")]
		public string RegistrationDateString
		{
			get
			{
				return this.RegistrationDate.ToString("yyyy-MM-dd");
			}
			set
			{
				this.RegistrationDate = DateTime.Parse(value);
			}
		}

		[XmlIgnore]
		public DateTime PublishDate { get; set; }
		[XmlElement("publish_date")]
		public string PublishDateString
		{
			get
			{
				return this.PublishDate.ToString("yyyy-MM-dd");
			}
			set
			{
				this.PublishDate = DateTime.Parse(value);
			}
		}
	}
}
