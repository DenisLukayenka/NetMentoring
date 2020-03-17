using Pacific.Core.EventData;
using Pacific.Core.Services;
using System;
using System.Reflection;

namespace Parky.ConsoleTest
{
	class Program
	{
		static void Main(string[] args)
		{
			/*Console.WriteLine("Hello World!");

			IContainer container = new Container();
			//container.AddAssembly(Assembly.GetExecutingAssembly());
			
			container.AddType(typeof(TestModel));
			container.AddType(typeof(SubModel));
			container.AddType(typeof(PropertyModel));

			container.AddType<B, A>();
			
			var result = container.CreateInstance<TestModel>();
			var b = container.CreateInstance<A>();

			Console.WriteLine(b.Prop);*/

			var visitor = new FileSystemVisitor(@"C:\Users\dzianis_lukayenka\Desktop\New folder\ClientApp\src");
			visitor.OnFinishExplore += Finish;
			visitor.OnStartExplore += Start;

			foreach(var info in visitor.Explore())
			{
				Console.WriteLine(info.FullName);
			}
		}

		static void Start(object sender, StartExploreEventArgs e)
		{
			Console.WriteLine("---------------------------------------");
			Console.WriteLine($"Start position: {e.StartPosition}");
			Console.WriteLine("---------------------------------------");
		}

		static void Finish(object sender, FinishExploreEventArgs e)
		{
			Console.WriteLine("---------------------------------------");
			Console.WriteLine($"Start position: {e.StartPosition}");
			Console.WriteLine($"Files count: {e.FilesCount}");
			Console.WriteLine($"Directories count: {e.DirectoriesCount}");
			Console.WriteLine("---------------------------------------");
		}
	}
}
