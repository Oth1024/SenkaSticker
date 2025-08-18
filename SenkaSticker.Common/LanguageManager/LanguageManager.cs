namespace SenkaSticker.Common.LanguageManager
{
    public class LanguageManager : Singleton<LanguageManager>, ILanguageManager
    {
        #region Constructor
        private LanguageManager()
        {
            CurrentLanguage = Languages.ZhCn;
        }
        #endregion

        #region Fields
        #endregion

        #region Properties
        public Languages CurrentLanguage { get; private set; }
        #endregion

        #region ILanguageManager Implementation
        public void SetCurrentLanguage(Languages language)
        {
            CurrentLanguage = language;
        }

        public Languages GetCurrentLanguage()
        {
            return CurrentLanguage;
        }
        #endregion

        #region Private Methods
        #endregion
    }
}
