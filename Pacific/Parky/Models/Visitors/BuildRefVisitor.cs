using System;
using System.Collections.Generic;
using System.Reflection;
using Parky.Models.Builders;
using Parky.Utilities;

namespace Parky.Models.Visitors
{
    public class BuildRefVisitor : IVisitor
    {
        private IBuilder _builder;
        protected IDictionary<Type, Ref> _refs;

        public BuildRefVisitor(IBuilder builder, IDictionary<Type, Ref> refs)
        {
            this._builder = builder;
            this._refs = refs;
        }

        public object VisitConstructorRef(ConstructorRef @ref)
        {
            return this.FillCtorParams(@ref);
        }

        public object VisitExportRef(ExportRef @ref)
        {
            return this._builder.BuildObject(@ref.Type, null);
        }

        public object VisitFullRef(Ref @ref)
        {
            object result = this.FillCtorParams(@ref);
            this.FillFieldsParams(@ref, result);
            this.FillPropertiesParams(@ref, result);

            return result;
        }

        public object VisitImportRef(ImportRef @ref)
        {
            object result = this._builder.BuildObject(@ref.Type, null);
            this.FillFieldsParams(@ref, result);
            this.FillPropertiesParams(@ref, result);

            return result;
        }

        protected object FillCtorParams(Ref @ref)
        {
            var ctorRefs = @ref.Type.GetAllConstructorsRefs();
            object[] objParams = new object[ctorRefs.Length];

            for(int i = 0; i < ctorRefs.Length; i++)
            {
                var paramObj = this.CreateInstance(ctorRefs[i]);
                objParams[i] = paramObj;
            }

            return this._builder.BuildObject(@ref.Type, objParams);
        }

        protected object FillPropertiesParams(Ref @ref, object target)
        {
            var propertiesRefs = @ref.Type.GetRegisteredProperties(this._refs);

			object propertyTempRef;
			foreach(PropertyInfo propertyInfo in propertiesRefs)
			{
				if(propertyInfo.GetValue(target) is null)
				{
					propertyTempRef = this.CreateInstance(propertyInfo.PropertyType);
					propertyInfo.SetValue(target, propertyTempRef);
				}
			}

			return target;
        }

        protected object FillFieldsParams(Ref @ref, object target)
        {
			var fieldsRefs = @ref.Type.GetRegisteredFields(this._refs);
			
			object fieldTempRef;
			foreach(FieldInfo fieldInfo in fieldsRefs)
			{
				if(fieldInfo.GetValue(target) is null)
				{
					fieldTempRef = this.CreateInstance(fieldInfo.FieldType);
					fieldInfo.SetValue(target, fieldTempRef);
				}
			}

			return target;
        }

        protected object CreateInstance(Type type)
        {
            if(this._refs.TryGetValue(type, out var @ref))
            {
                return @ref.Build(this);
            }

            throw new Exception($"The type: {type.FullName} is not registered.");
        }
    }
}