using Avalonia;
using Avalonia.Interactivity;
using Avalonia.Styling;
using DialogHostAvalonia;
using SenkaSticker.Common.TypeDef.Enum;
using SenkaSticker.Controls.Base;
using SenkaSticker.Controls.Components.Parts.ViewModels;
using SenkaSticker.Controls.Model;
using System.Collections.ObjectModel;

namespace SenkaSticker.Controls.Components.ViewModels;

public class DefaultSenkaGridViewModel : ViewModelBase
{
    #region Constructor
    public DefaultSenkaGridViewModel()
    {
        Issues = new();
        Messages = new();
    }
    #endregion

    #region Fields
    #endregion

    #region Properties
    public IssuePopViewModel Issues { get; set; }

    public MessagePopViewModel Messages { get; set; }
    #endregion

    #region Methods
    #endregion
}
