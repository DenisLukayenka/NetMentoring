using System;

namespace Parky.Models.Builders
{
    public class ObjectBuilder : IBuilder
    {
        public object BuildObject(Type type, object[] arguments)
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