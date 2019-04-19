using System;
using Dynamitey;

namespace MyTvSeries.Framework
{
    public class TypeCaster : ITypeCaster
    {
        public dynamic CastToDerivedType(object obj)
        {
            Type type = obj.GetType();
            return CastToType(obj, type);
        }

        public dynamic CastToType(object obj, Type type)
        {
            return Dynamic.InvokeConvert(obj, type, true);
        }
    }
}
