using System;
using System.Reflection;
using Parky.Models;
using System.Collections.Generic;
using Parky.Attributes;
using Parky.Utilities;
using Parky.Models.Visitors;
using Parky.Models.Builders;

namespace Parky
{
	public class Container : IContainer
	{
		private IDictionary<Type, Ref> _refs;
		private IBuilder _builder;
		private IVisitor _buildVisitor;

		public Container()
		{
			this._refs = new Dictionary<Type, Ref>();
			this._builder = new ObjectBuilder();
			this._buildVisitor = new BuildRefVisitor(this._builder, this._refs);
		}

		public void AddAssembly(Assembly asm)
		{
			/*Type[] types = asm.GetTypes();

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
			}*/
		}

		public void AddType<T>()
		{
			this.AddType(typeof(T));
		}

		public void AddType(Type type)
		{
			if(!this._refs.TryGetValue(type, out var instance))
			{
				this._refs.Add(type, new Ref(type));
			}
		}

		public void AddType<T, IT>() where T: IT
		{
			this.AddType(typeof(T), typeof(IT));
		}

		public void AddType(Type type, Type baseType)
		{
			if(!this._refs.TryGetValue(baseType, out var instance))
			{
				this._refs.Add(baseType, new Ref(type, baseType));
			}
		}

		public T CreateInstance<T>()
		{
			return (T)this.CreateInstance(typeof(T));
		}

		public object CreateInstance(Type type)
		{
			if(this._refs.TryGetValue(type, out var @ref))
			{
				return @ref.Build(this._buildVisitor);
			}
			
			throw new ApplicationException($"Type: {type.FullName} was not found!");
		}
	}
}
