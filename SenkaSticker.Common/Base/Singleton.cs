using System.Reflection;

namespace SenkaSticker.Common.Base
{
    public abstract class Singleton<T>
    {
        #region Fields
        private static readonly object _instanceLock = new object();

        private static T _instance;
        #endregion

        #region Properties
        public static T Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    var type = typeof(T);
                    var constructor = type.GetConstructor(
                        BindingFlags.Instance | BindingFlags.NonPublic,
                        null,
                        Type.EmptyTypes,
                        null);
                    if (constructor == null)
                    {
                        throw new Exception($"Can not find private constructor for type[{type}]");
                    }
                    _instance = (T)constructor.Invoke(null);
                    return _instance;
                }
            }
        }
        #endregion
    }
}
