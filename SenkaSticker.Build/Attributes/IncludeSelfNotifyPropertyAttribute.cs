using System;
using System.Collections;
using System.Linq;
using System.Threading;

namespace SenkaSticker.Build.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public sealed class IncludeSelfNotifyPropertyAttribute : Attribute
{
    #region Constructor
    public IncludeSelfNotifyPropertyAttribute(Type propertyType, string propertyName)
    {
        PropertyType = propertyType;
        PropertyName = propertyName;
        AssertNotNull();
    }
    #endregion

    #region Events
    #endregion

    #region Fields
    #endregion

    #region Properties
    public Type PropertyType { get; }

    public string PropertyName { get; }
    #endregion

    #region Methods
    private void AssertNotNull()
    {
        if (PropertyType == null || string.IsNullOrEmpty(PropertyName))
        {
            throw new Exception($"Cannot regist SelfNotify Property with null argument.");
        }
    }
    #endregion
}
