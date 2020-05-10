using System.IO;
using System.Xml.Serialization;
using System.Text;

namespace Task.Tests.TestHelpers
{
	public class XmlBasicSerializerTester<T> : SerializationTester<T, XmlSerializer>
	{
		public XmlBasicSerializerTester(XmlSerializer serializer, bool showResult = false) 
			: base(serializer, showResult)
		{
		}

		internal T Deserialization()
		{
			return this.Deserialization(this.GetTargetStream());
		}

		internal void Serialization(T data)
		{
			this.Serialization(data, new MemoryStream());
		}

		internal override T Deserialization(MemoryStream stream)
		{
			return (T)serializer.Deserialize(stream);
		}

		internal override void Serialization(T data, MemoryStream stream)
		{
			serializer.Serialize(stream, data);
		}

		protected override MemoryStream GetTargetStream()
		{
			using(var fileStream = new StreamReader(@"..\..\books.xml"))
			{
				return new MemoryStream(Encoding.UTF8.GetBytes(fileStream.ReadToEnd()));
			}
		}
	}
}
