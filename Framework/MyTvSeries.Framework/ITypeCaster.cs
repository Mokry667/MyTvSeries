using System;

namespace MyTvSeries.Framework
{
    public interface ITypeCaster
    {
        dynamic CastToDerivedType(object obj);

        dynamic CastToType(object obj, Type type);
    }
}
