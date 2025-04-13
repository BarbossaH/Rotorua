using System;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class AutoRegisterRepositoryAttribute : Attribute
{
    public Type InterfaceType { get; }

    public AutoRegisterRepositoryAttribute(Type interfaceType)
    {
        InterfaceType = interfaceType;
    }
}
