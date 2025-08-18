namespace SenkaSticker.Common.LanguageManager
{
    public enum Languages
    {
        ZhCn,
        EnUs
    }

    public static class LanguagesExtension
    {
        public static string GetLanguageDisplayName(this Languages language)
        {
            return language switch
            {
                Languages.ZhCn => "zh-cn",
                Languages.EnUs => "en-us"
            };
        }

        public static string GetLanguageString(this Languages language)
        {
            return language switch
            {
                Languages.ZhCn => "ZhCn",
                Languages.EnUs => "EnUs"
            };
        }
    }
}
