using System;
using System.Reflection;

namespace Parky
{
	public interface IContainer
	{
		void AddType<T>();
		void AddType(Type type);


		void AddType<T, IT>() where T: IT;
		void AddType(Type type, Type baseType);

		void AddAssembly(Assembly asm);

		T CreateInstance<T>();

		object CreateInstance(Type type);
	}
}
