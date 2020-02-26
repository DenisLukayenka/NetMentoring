using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Parky.Models;

namespace Parky.Utilities
{
    public static class TypeExtensions
    {
        public static Type[] GetAllConstructorsRefs(this Type type)
        {
            return type.GetConstructors()
							.SelectMany(c => c.GetParameters())
							.Select(p => p.ParameterType)
                            .ToArray(); 
        }

        public static PropertyInfo[] GetRegisteredProperties(this Type type, IDictionary<Type, Ref> refs)
        {
            return type.GetProperties().Where(p => refs.ContainsKey(p.PropertyType)).ToArray();
        }

        public static FieldInfo[] GetRegisteredFieds(this Type type, IDictionary<Type, Ref> refs)
        {
            return type.GetFields().Where(p => refs.ContainsKey(p.FieldType)).ToArray();
        }

        public static PropertyInfo[] GetAllProperties(this Type type)
        {
            return type.GetProperties().ToArray();
        }

        public static FieldInfo[] GetAllFields(this Type type)
        {
            return type.GetFields().ToArray();
        }
    }
}