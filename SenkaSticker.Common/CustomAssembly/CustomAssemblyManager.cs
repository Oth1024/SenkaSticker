using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using SenkaSticker.Common.Base;

namespace SenkaSticker.Common.CustomAssembly;

public class CustomAssemblyManager
{
    #region Constructor
    private CustomAssemblyManager()
    {

    }
    #endregion

    #region Fields
    private List<Assembly> _guiAssemblies = new();
    private List<Assembly> _extensionAssemblies = new();
    #endregion

    #region Singleton
    public static CustomAssemblyManager Instance => Singleton<CustomAssemblyManager>.Instance;
    #endregion

    #region Methods
    public List<Assembly> GetGuiAssemblies()
    {
        return new List<Assembly>(_guiAssemblies);
    }

    public List<Assembly> GetExtensionAssemblies()
    {
        return new List<Assembly>(_extensionAssemblies);
    }

    public void AddGuiAssembly(Assembly assembly)
    {
        _guiAssemblies.Add(assembly);
    }

    public void AddGuiAssemblies(IEnumerable<Assembly> assemblies)
    {
        _guiAssemblies.AddRange(assemblies);
    }

    public void AddExtensionAssembly(Assembly assembly)
    {
        _extensionAssemblies.Add(assembly);
    }

    public void AddExtensionAssemblies(IEnumerable<Assembly> assemblies)
    {
        _extensionAssemblies.AddRange(assemblies);
    }
    #endregion
}
