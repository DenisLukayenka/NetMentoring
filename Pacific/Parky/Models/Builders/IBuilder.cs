using System;

namespace Parky.Models.Builders
{
    public interface IBuilder
    {
        object BuildObject(Type type, object[] arguments);
    }
}