using System;
using System.Reflection;
using Parky.Models;
using System.Linq;
using System.Collections.Generic;

namespace Parky
{
	public class Container : IContainer
	{
		private IDictionary<Type, Ref> _refs;

		public Container()
		{
			this._refs = new Dictionary<Type, Ref>();
		}

		public void AddAssembly(Assembly asm)
		{
			throw new NotImplementedException();
		}

		public void AddType(Type type)
		{
			if(!this._refs.TryGetValue(type, out var instance))
			{
				this._refs.Add(type, new Ref(type));
			}
		}

		public void AddType(Type type, Type baseType)
		{
			throw new NotImplementedException();
		}

		public T CreateInstance<T>()
		{
			return (T)this.CreateInstance(typeof(T));
		}

		public object CreateInstance(Type type)
		{
			if(this._refs.TryGetValue(type, out var @ref))
			{
				object result = null;

				if(@ref.IsConstructorInit)
				{
					result = this.InitializeObject(@ref.Type);
				} 
				if(@ref.IsPropertiesInit)
				{
					result = this.InitializeProperties(@ref.Type, result);
				}
				
				if(!@ref.IsConstructorInit && !@ref.IsPropertiesInit)
				{
					result = this.CreateInst(@ref.Type, null);
				}

				return result;
			}
			
			throw new ApplicationException($"Type: {type.FullName} was not found!");
		}

		private object InitializeObject(Type type)
		{
			var instanceRefs = this.GetConstructorRefs(type).ToArray();
			if(instanceRefs.Length == 0)
			{
				return this.CreateInst(type, null);
			}

			object[] objParams = new object[instanceRefs.Length];
			for(int i = 0; i < instanceRefs.Length; i++)
			{
				var paramObj = this.CreateInstance(instanceRefs[i]);
				objParams[i] = paramObj;
			}

			return this.CreateInst(type, objParams);
		}

		private object InitializeProperties(Type type, object obj)
		{
			obj = obj is null? this.CreateInst(type, null) : obj;

			var propertiesRefs = this.GetProperties(type).ToArray();
			if(propertiesRefs.Length == 0)
			{
				return obj;
			}

			object propertyTempRef;
			foreach(PropertyInfo propertyInfo in propertiesRefs)
			{
				if(propertyInfo.GetValue(obj) == null)
				{
					propertyTempRef = this.CreateInstance(propertyInfo.PropertyType);
					propertyInfo.SetValue(obj, propertyTempRef);
				}
			}

			return obj;
		}

		private IEnumerable<Type> GetConstructorRefs(Type type)
		{
			return type.GetConstructors()
							.SelectMany(c => c.GetParameters())
							.Select(p => p.ParameterType);
		}

		private IEnumerable<PropertyInfo> GetProperties(Type type)
		{
			return type.GetProperties().Where(p => this._refs.ContainsKey(p.PropertyType));
		}

		private object CreateInst(Type type, object[] arguments)
		{
			object result = null;

			try
			{
				result = Activator.CreateInstance(type, arguments);
			}
			catch(Exception ex)
			{
				throw new Exception($"Cannot create instance of type: {type.FullName}.");
			}

			return result;
		}
	}
}
