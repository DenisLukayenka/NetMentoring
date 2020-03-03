using System;
using System.Reflection;

namespace Parky.ConsoleTest
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			IContainer container = new Container();
			//container.AddAssembly(Assembly.GetExecutingAssembly());
			
			container.AddType(typeof(TestModel));
			container.AddType(typeof(SubModel));
			container.AddType(typeof(PropertyModel));

			container.AddType<B, A>();
			
			var result = container.CreateInstance<TestModel>();
			var b = container.CreateInstance<A>();

			Console.WriteLine(b.Prop);
		}
	}
}
