using System;
using System.Reflection;

namespace Parky
{
	public interface IContainer
	{
		void AddType(Type type);

		void AddType(Type type, Type baseType);

		void AddAssembly(Assembly asm);

		T CreateInstance<T>();

		object CreateInstance(Type type);
	}
}
