using DialogHostAvalonia;
using SenkaSticker.Controls.Base;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using SenkaSticker.Common.Base;

namespace SenkaSticker.Controls.Common
{
    public class DialogManager
    {
        #region Constructor
        private DialogManager()
        {

        }
        #endregion

        #region Events
        #endregion

        #region Fields
        private ConcurrentDictionary<DialogViewModelBase, string> _dialogAndId = new();
        #endregion

        #region Properties
        public static DialogManager Instance => Singleton<DialogManager>.Instance;
        #endregion

        #region Methods
        public Task<object?> Show(DialogViewModelBase dialogViewModelBase)
        {
            var result =  DialogHost.Show(dialogViewModelBase, dialogViewModelBase.DialogId);
            _dialogAndId[dialogViewModelBase] = dialogViewModelBase.DialogId;
            return result;
        }

        public async void ShowAsync(DialogViewModelBase dialogViewModelBase)
        {
            await DialogHost.Show(dialogViewModelBase, dialogViewModelBase.DialogId);
            _dialogAndId[dialogViewModelBase] = dialogViewModelBase.DialogId;
        }

        public void Close(DialogViewModelBase dialogViewModelBase, bool panic = false,[CallerMemberName] string methodName = null)
        {
            if (_dialogAndId.TryGetValue(dialogViewModelBase, out var dialogId))
            {
                DialogHost.Close(dialogId, panic);
            }
            else
            {
                throw new Exception($"Can not get DialogId from caller[{methodName}].");
            }
        }

        public void Close(string dialogId, bool panic = false)
        {
            DialogHost.Close(dialogId);
            var dialog = _dialogAndId.FirstOrDefault(x => x.Value == dialogId).Key;
            if (panic && dialog != null)
            {
                _dialogAndId.TryRemove(dialog, out _);
            }
        }
        #endregion
    }
}
