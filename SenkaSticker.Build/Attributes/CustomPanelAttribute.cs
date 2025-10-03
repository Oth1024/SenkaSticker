using System;
using System.Reflection;

namespace SenkaSticker.Build.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public class CustomPanelAttribute : Attribute
{
    #region
    public CustomPanelAttribute()
    {

    }
    #endregion
}
