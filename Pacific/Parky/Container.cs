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
			Type[] types = asm.GetTypes();

			foreach(var type in types)
			{
				if(this._refs.ContainsKey(type))
				{
					continue;
				}

				// Load refs for ImportConstructor attribute
				Attribute attrObj = type.GetCustomAttribute(typeof(ImportConstructorAttribute));
				if(attrObj != null)
				{
					this._refs.Add(type, new ConstructorRef(type));
					continue;
				}

				// Else load refs for export attribute
				attrObj = type.GetCustomAttribute(typeof(ExportAttribute));
				if(attrObj != null)
				{
					var exportAttr = attrObj as ExportAttribute;

					if(exportAttr.Contract != null)
					{
						this._refs.Add(exportAttr.Contract, new ExportRef(type, exportAttr.Contract));
					}
					else
					{
						this._refs.Add(type, new ExportRef(type));
					}
					
					continue;
				}
				
				
				// Load refs for properties and fields
				PropertyInfo[] properties = type.GetRegisteredProperties(this._refs);
				FieldInfo[] fields = type.GetRegisteredFields(this._refs);

				if(properties.Length > 0 || fields.Length > 0)
				{
					this._refs.Add(type, new ImportRef(type));
					this.RegisterFields(fields);
					this.RegisterProperties(properties);
				}
			}
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

		private void RegisterProperties(PropertyInfo[] propertiesRefs)
		{
			object attrObj;

			foreach(PropertyInfo pf in propertiesRefs)
			{
				attrObj = pf.GetCustomAttribute(typeof(ImportAttribute));

				if(attrObj != null && !this._refs.TryGetValue(pf.PropertyType, out var propRef))
				{
					this._refs.Add(pf.PropertyType, new ExportRef(pf.PropertyType));
				}
			}
		}

		private void RegisterFields(FieldInfo[] fieldsRefs)
		{
			object attrObj;

			foreach(FieldInfo pf in fieldsRefs)
			{
				attrObj = pf.GetCustomAttribute(typeof(ImportAttribute));

				if(attrObj != null && !this._refs.TryGetValue(pf.FieldType, out var propRef))
				{
					this._refs.Add(pf.FieldType, new ExportRef(pf.FieldType));
				}
			}
		}
	}
}
