global using ILogger = log4net.ILog;
using Avalonia;
using Avalonia.Interactivity;
using Avalonia.Styling;
using log4net;
using SenkaSticker.Build.Attributes;
using SenkaSticker.Common.CustomAssembly;
using SenkaSticker.Common.Logger;
using SenkaSticker.Controls.Base.Custom;
using SenkaSticker.Controls.Components.ViewModels;

namespace SenkaSticker.Test.CustomPanelTest.ViewModels;

[CustomPanel]
public class DemoViewModel : CustomPanelViewModelBase
{
    #region Constructor
    public DemoViewModel()
    {
    }
    #endregion

    #region Fields
    private ILogger _logger = LoggerFactory.GetLogger(nameof(DemoViewModel));
    #endregion

    #region Methods
    #endregion
}
