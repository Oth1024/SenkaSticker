using SenkaSticker.Controls.Base.Interface;
using System;
using System.Collections;
using System.Linq;
using System.Threading;

namespace SenkaSticker.Controls.Base
{
    public abstract class DialogViewModelBase : ViewModelBase, IDialogViewModel
    {
        #region Constructor
        public DialogViewModelBase()
        {

        }
        #endregion

        #region Events
        #endregion

        #region Fields
        #endregion

        #region Properties
        public string DialogId { get; }
        #endregion

        #region Methods
        #endregion
    }
}
