namespace SenkaSticker.Controls.Common.Language;

public interface ILanguageManager
{
    #region Properties
    /// <summary>
    /// 提供界面访问资源索引器
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string this[string key] { get; }
    

    /// <summary>
    /// 当前语言
    /// </summary>
    public Languages CurrentLanguage { get; }
    #endregion

    /// <summary>
    /// 设置语言
    /// </summary>
    /// <param name="language"></param>
    public void SetCurrentLanguage(Languages language);


    /// <summary>
    /// 获取当前语言
    /// </summary>
    /// <returns></returns>
    public Languages GetCurrentLanguage();

    /// <summary>
    /// 获取当前语言下key对应的文本
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string GetString(string key);

    /// <summary>
    /// 获取指定语言下的key对应的文本
    /// </summary>
    /// <param name="key"></param>
    /// <param name="language"></param>
    /// <returns></returns>
    public string GetString(string key, Languages language);
}
