using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using SenkaSticker.Common.Base;
using SenkaSticker.Common.Config;
using SenkaSticker.Common.Consts;

namespace SenkaSticker.Common.Language
{
    public class LanguageManager : PropertyChangedBase, ILanguageManager
    {
        #region Constructor
        private LanguageManager()
        {
            CurrentLanguage = Languages.ZhCn;
            InitializeResource();
        }
        #endregion

        #region Fields
        private ConcurrentDictionary<Languages, ConcurrentDictionary<string, string>> _languageResources;
        #endregion

        #region Properties
        public static LanguageManager LanguageResource => Singleton<LanguageManager>.Instance;

        public string this[string key]
        {
            get
            {
                if (_languageResources.ContainsKey(CurrentLanguage)
                    && _languageResources[CurrentLanguage].TryGetValue(key, out string contentValue)
                    && !string.IsNullOrEmpty(contentValue))
                {
                    return contentValue;
                }
                return string.Empty;
            }
        }

        public Languages CurrentLanguage { get; private set; }
        #endregion

        #region ILanguageManager Implementation
        public void SetCurrentLanguage(Languages language)
        {
            CurrentLanguage = language;
            NotifyPropertyChanged(nameof(LanguageResource));
        }

        public Languages GetCurrentLanguage()
        {
            return CurrentLanguage;
        }

        public string GetString(string key)
        {
            return GetString(key, CurrentLanguage);
        }

        public string GetString(string key, Languages language)
        {
            if (_languageResources.TryGetValue(language, out var dic)
                && dic.TryGetValue(key, out var result))
            {
                return result;
            }
            return string.Empty;
        }
        #endregion

        #region Private Methods
        private void InitializeResource()
        {
            var config = ConfigManager.Instance.GetConfig<Dictionary<Languages, ConcurrentDictionary<string, string>>>(PathConst.LANGUAGE_CONFIG);
            if (config != null)
            {
                config = new Dictionary<Languages, ConcurrentDictionary<string, string>>();
                ConfigManager.Instance.RegistConfig<Dictionary<Languages, ConcurrentDictionary<string, string>>>(PathConst.LANGUAGE_CONFIG);
                _languageResources = new(config);
            }
        }
        #endregion
    }
}
