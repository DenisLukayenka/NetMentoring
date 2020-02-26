using System;
using System.Reflection;
using Parky.Models;
using System.Collections.Generic;
using Parky.Attributes;
using Parky.Utilities;

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
			Type[] types = asm.GetTypes();

			foreach(var type in types)
			{
				object attrObj = type.GetCustomAttribute(typeof(ImportConstructorAttribute));
				
				// Load refs for ImportConstructor attribute
				if(attrObj != null)
				{
					// Check if reference is already registered, if true then change type of ref to relevant.
					if(this._refs.TryGetValue(type, out var value))
					{
						if(value.IsPropertiesInit && !value.IsConstructorInit)
						{
							this._refs[type] = new Ref(type);
						}
						else if(!value.IsConstructorInit && !value.IsPropertiesInit)
						{
							this._refs[type] = new ConstructorRef(type);
						}
					}
					else
					{
						this._refs.Add(type, new ConstructorRef(type));
					}
				}
				else
				{
					// Else load refs for export attribute
					attrObj = type.GetCustomAttribute(typeof(ExportAttribute));

					if(attrObj != null && !this._refs.TryGetValue(type, out var value))
					{
						this._refs.Add(type, new ExportRef(type));
					}
				}
				
				// Load refs for properties and fields
				var propertiesFieldsRefs = type.GetAllProperties();
				foreach(var pf in propertiesFieldsRefs)
				{
					attrObj = pf.GetCustomAttribute(typeof(ImportAttribute));

					if(attrObj != null && this._refs.TryGetValue(type, out var value))
					{
						if(!value.IsPropertiesInit && value.IsConstructorInit)
						{
							this._refs[type] = new Ref(type);
						}
						else if(!value.IsConstructorInit && !value.IsPropertiesInit)
						{
							this._refs[type] = new PropertyRef(type);
						}

						if(!this._refs.TryGetValue(pf.PropertyType, out var propRef))
						{
							this._refs.Add(pf.PropertyType, new ExportRef(pf.PropertyType));
						}
					}
				}
			}
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
			var instanceRefs = type.GetAllConstructorsRefs();
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

			var propertiesRefs = type.GetRegisteredProperties(this._refs);
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

		private object CreateInst(Type type, object[] arguments)
		{
			object result = null;

			try
			{
				result = Activator.CreateInstance(type, arguments);
			}
			catch(Exception ex)
			{
				throw new Exception($"Cannot create instance of type: {type.FullName}.", ex);
			}

			return result;
		}
	}
}
