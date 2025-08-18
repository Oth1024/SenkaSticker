namespace SenkaSticker.Common.LanguageManager
{
    public interface ILanguageManager
    {
        public Languages CurrentLanguage { get; }

        public void SetCurrentLanguage(Languages language);

        public Languages GetCurrentLanguage();
    }
}
