using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskSerialization.DB;
using System.Linq;
using Task.Tests.TestHelpers;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TaskSerialization.Helpers;
using System.Data.Entity.Infrastructure;

namespace Task.Tests
{
	[TestClass]
	public class SerializationTests
	{
		IObjectContextAdapter contextAdapter;
		Northwind dbContext;

		[TestInitialize]
		public void Initialize()
		{
			dbContext = new Northwind();
			contextAdapter = (dbContext as IObjectContextAdapter)?.ObjectContext;
		}

		[TestMethod]
		public void SerializationCallbacks()
		{
			dbContext.Configuration.ProxyCreationEnabled = false;
			var serializer = new NetDataContractSerializer(new StreamingContext(StreamingContextStates.All, contextAdapter));

			var tester = new XmlDataContractSerializerTester<IEnumerable<Category>>(serializer, true);
			var categories = dbContext.Categories.ToList();

			var c = categories.First();

			var result = tester.SerializeAndDeserialize(categories);

			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void ISerializable()
		{
			dbContext.Configuration.ProxyCreationEnabled = false;

			var serializer = new NetDataContractSerializer(new StreamingContext(StreamingContextStates.All, contextAdapter));

			var tester = new XmlDataContractSerializerTester<IEnumerable<Product>>(serializer, true);
			var products = dbContext.Products.ToList();

			var result = tester.SerializeAndDeserialize(products);
			Assert.IsNotNull(result);
		}


		[TestMethod]
		public void ISerializationSurrogate()
		{
			dbContext.Configuration.ProxyCreationEnabled = false;

			var serializer = new NetDataContractSerializer()
			{
				SurrogateSelector = new OrderDetailSurrogateSelector(),
				Context = new StreamingContext(StreamingContextStates.All, contextAdapter),
			};

			var tester = new XmlDataContractSerializerTester<IEnumerable<Order_Detail>>(serializer, true);
			var orderDetails = dbContext.Order_Details.ToList();

			var result = tester.SerializeAndDeserialize(orderDetails);
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void IDataContractSurrogate()
		{
			dbContext.Configuration.ProxyCreationEnabled = true;
			dbContext.Configuration.LazyLoadingEnabled = true;

			var serializer = new DataContractSerializer(typeof(IEnumerable<Order>), new DataContractSerializerSettings { DataContractSurrogate = new OrderDataContractSurrogate() });
			
			var tester = new XmlDataContractSerializerTester<IEnumerable<Order>>(serializer, true);
			var orders = dbContext.Orders.ToList();

			var result = tester.SerializeAndDeserialize(orders);
			Assert.IsNotNull(result);
		}
	}
}
