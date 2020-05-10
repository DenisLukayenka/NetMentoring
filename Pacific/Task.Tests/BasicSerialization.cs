using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task.Tests.TestHelpers;
using TaskSerialization.BookModels;
using System.Xml.Serialization;
using System;
using System.IO;

namespace Task.Tests
{
	[TestClass]
	public class BasicSerialization
	{
		[TestMethod]
		public void SerializationCallbacks()
		{
			var tester = new XmlBasicSerializerTester<Catalog>(new XmlSerializer(typeof(Catalog)), true);

			var catalog = tester.Deserialization();
			Assert.IsNotNull(catalog);
		}

		[TestMethod]
		public void SerializeAndDeserialize()
		{
			var tester = new XmlBasicSerializerTester<Catalog>(new XmlSerializer(typeof(Catalog)), true);
			var stream = new MemoryStream();

			var resultCatalog = tester.SerializeAndDeserialize(this.GetCatalog(), stream);

			Assert.IsNotNull(resultCatalog);
		}

		private Catalog GetCatalog()
		{
			return new Catalog
			{
				Date = DateTime.Now,
				Books = new List<Book>
				{
					new Book
					{
						Id = "bk101",
						IsBn = "0-596-00103-7",
						Author = "Löwy, Juval",
						Title = "COM and .NET Component Services",
						Genre = "Computer science",
						Publisher = "O'Reilly",
						PublishDate = DateTime.Now,
						Description = "gfdshfdsjh",
						RegistrationDate = DateTime.Now,
					},
					new Book
					{
						Id = "bk102",
						Author = "Ralls, Kim",
						Title = "Midnight Rain",
						Genre = "Fantasy",
						Publisher = "R &amp; D",
						PublishDate = DateTime.Parse("2000-12-16"),
						Description = "A former architect battles corporate zombies",
						RegistrationDate = DateTime.Parse("2017-01-01"),
					},
				}
			};
		}
	}
}
