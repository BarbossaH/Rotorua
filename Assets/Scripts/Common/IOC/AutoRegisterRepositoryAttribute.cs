namespace Common
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class TypeCategoryAttribute : Attribute
    {
        public TypeCategoryEnum Category { get; }
        public Type InterfaceType { get; }

        public TypeCategoryAttribute(TypeCategoryEnum category, Type interfaceType)
        {
            Category = category;
            InterfaceType = interfaceType;
        }
    }

    // [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    // public class AutoRegisterRepositoryAttribute : Attribute
    // {
    //     public Type InterfaceType { get; }

    //     public AutoRegisterRepositoryAttribute(Type interfaceType)
    //     {
    //         InterfaceType = interfaceType;
    //     }
    // }
}