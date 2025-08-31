using Avalonia;
using Avalonia.Interactivity;
using Avalonia.Styling;
using DialogHostAvalonia;
using SenkaSticker.Controls.Base;
using SenkaSticker.Controls.Common;

namespace SenkaSticker.Controls.Components.Parts.ViewModels
{
    public class CloseButtonViewModel : ViewModelBase
    {
        #region Constructor
        /// <summary>
        /// Attach a close button to a dialog 
        /// </summary>
        /// <param name="parent"></param>
        public CloseButtonViewModel(DialogViewModelBase? parent = null)
        {
            _parent = parent;
        }
        #endregion

        #region Events
        #endregion

        #region Fields
        private readonly DialogViewModelBase? _parent;
        #endregion


        #region Properties
        #endregion

        #region Methods
        public void Close()
        {
            if (_parent != null)
            {
                DialogManager.Instance.Close(_parent);
            }
            else
            {
                Environment.Exit(0);
            }
        }
        #endregion
    }
}
