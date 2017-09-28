using System;
using System.ComponentModel;

namespace nemonic
{
    //추상화 콘트롤을 상속받으면서, 디자이너를 이용하기 위한 리플랙션 속성 Class
    internal class AbstractControlDescriptionProvider<TAbstract, TBase> : TypeDescriptionProvider
    {
        public AbstractControlDescriptionProvider() : base(TypeDescriptor.GetProvider(typeof(TAbstract)))
        {

        }

        public override Type GetReflectionType(Type objectType, object instance)
        {
            if (objectType == typeof(TAbstract))
            {
                return typeof(TBase);
            }

            return base.GetReflectionType(objectType, instance);
        }

        public override object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, object[] args)
        {
            if (objectType == typeof(TAbstract))
            {
                objectType = typeof(TBase);
            }

            return base.CreateInstance(provider, objectType, argTypes, args);
        }
    }
}
