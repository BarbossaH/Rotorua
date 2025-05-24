namespace Common
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class AutoRegisterRepositoryAttribute : Attribute
    {
        public Type ClassNameType { get; }

        public AutoRegisterRepositoryAttribute(Type interfaceType)
        {
            // Category = category;
            ClassNameType = interfaceType;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class AutoRegisterFactoryAttribute : Attribute
    {
        public Type InterfaceType { get; }

        public AutoRegisterFactoryAttribute(Type interfaceType)
        {
            InterfaceType = interfaceType;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class AutoRegisterManagerAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Class)]
    public class AutoRegisterIDGeneratorAttribute : Attribute
    {
        public Type InterfaceType { get; }

        public AutoRegisterIDGeneratorAttribute(Type interfaceType)
        {
            InterfaceType = interfaceType;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class AutoRegisterControllerAttribute : Attribute
    {
        public Type InterfaceType { get; }
        public AutoRegisterControllerAttribute(Type interfaceType)
        {
            InterfaceType = interfaceType;
        }
    }
}