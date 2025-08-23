using System.ComponentModel;
using System.Reflection;
using Avalonia.Threading;

namespace SenkaSticker.Common.Base
{
    public abstract class PropertyChangedBase : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region Methods
        public void NotifyPropertyChanged()
        {
            var type = this.GetType();
            var properties = type.GetProperties(BindingFlags.Public);
            foreach (var item in properties)
            {
                OnUIThread(() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(item.Name)));
            }
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            OnUIThread(() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)));
        }

        protected void OnUIThread(Action action)
        {
            Dispatcher.UIThread.Invoke(action);
        }
        #endregion
    }
}
