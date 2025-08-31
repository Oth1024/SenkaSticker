using SenkaSticker.Common.CustomAssembly;
using SenkaSticker.Controls;
using SenkaSticker.Controls.Base;
using SenkaSticker.Controls.Base.Custom;
using SenkaSticker.Controls.Components.ViewModels;
using System;
using System.Linq;
using System.Reflection;

namespace SenkaSticker.ViewModels;

public class MainViewModel : ViewModelBase
{
    #region Constructor
    public MainViewModel()
    {
        GetDefaultOrCustomPanel();
    }
    #endregion

    #region Fields
    #endregion

    #region Properties
    public ViewModelBase DisplayPanel { get; private set; }
    #endregion

    #region Methods
    private void GetDefaultOrCustomPanel()
    {
        var extensions = CustomAssemblyManager.Instance.GetExtensionAssemblies();
        ViewModelBase toDisplayPanel = null;
        var customBaseType = typeof(CustomPanelViewModelBase);
        foreach (var extension in extensions)
        {
            var types = extension.GetTypes();
            foreach (var type in types)
            {
                // 检查该类为CustomPanelViewModelBase的一个子类，并且并非一个抽象类或虚拟类
                if (type.IsAssignableTo(customBaseType) && type.GetCustomAttributes(false).Any(attr => attr is CustomPanelAttribute))
                {
                    var constructor = type.GetConstructor(
                        BindingFlags.Instance | BindingFlags.Public,
                        null,
                        Type.EmptyTypes,
                        null);
                    if (constructor == null)
                    {
                        throw new Exception($"Can not construct custom panel of type[{type}] " +
                            $"because a public and non-param constructor is required.");
                    }
                    toDisplayPanel = constructor.Invoke(null) as ViewModelBase;
                    break;
                }
            }
            if (toDisplayPanel != null)
            {
                break;
            }
        }
        if (toDisplayPanel == null)
        {
            DisplayPanel = new DefaultSenkaGridViewModel();
        }
        else
        {
            DisplayPanel = toDisplayPanel;
        }
    }
    #endregion
}
