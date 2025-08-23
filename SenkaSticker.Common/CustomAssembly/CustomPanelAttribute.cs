using System;
using System.Reflection;

namespace SenkaSticker.Common.CustomAssembly;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public class CustomPanelAttribute : Attribute
{
    #region
    public CustomPanelAttribute()
    {

    }
    #endregion
}
