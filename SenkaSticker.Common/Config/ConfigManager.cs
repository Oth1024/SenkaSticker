using Newtonsoft.Json;
using SenkaSticker.Common.Base;
using SenkaSticker.Common.Consts;
using System.Collections.Concurrent;
using System.Reflection;

namespace SenkaSticker.Common.Config;

public class ConfigManager
{
    #region Constructor
    private ConfigManager()
    {
        Initialize();
    }
    #endregion

    #region Singleton
    public static ConfigManager Instance => Singleton<ConfigManager>.Instance;
    #endregion

    #region Fields
    private string _configPath;
    private ConcurrentDictionary<string, object> _configs = new();
    #endregion

    #region Public Methods
    public TConfig? GetConfig<TConfig>(string configName) where TConfig : class
    {
        if (_configs.TryGetValue(configName, out var result))
        {
            return result as TConfig;
        }
        var config = Deserialize<TConfig>(configName);
        if (config != null)
        {
            _configs[configName] = config;
        }
        return config;
    }

    public async Task<TConfig?> GetConfigAsync<TConfig>(string configName) where TConfig : class
    {
        if (_configs.TryGetValue(configName, out var result))
        {
            return result as TConfig;
        }
        var config = await DeserializeAsync<TConfig>(configName);
        if (config != null)
        {
            _configs[configName] = config;
        }
        return config;
    }

    /// <summary>
    /// Config will be saved and read at "./Configs/{configName}"
    /// </summary>
    /// <typeparam name="TConfig"></typeparam>
    /// <param name="configName"></param>
    public void RegistConfig<TConfig>(string configName) where TConfig : class
    {
        Serialize<TConfig>(configName, null);
    }

    /// <summary>
    /// Config will be intialized with a default instance given by {config} and saved at "./Configs/{configName}"
    /// </summary>
    /// <typeparam name="TConfig"></typeparam>
    /// <param name="configName"></param>
    public void RegistConfig<TConfig>(string configName, TConfig config) where TConfig : class
    {
        Serialize<TConfig>(configName, config);
    }

    /// <summary>
    /// Config will be saved and read at "./Configs/{configName}"
    /// </summary>
    /// <typeparam name="TConfig"></typeparam>
    /// <param name="configName"></param>
    public async void RegistConfigAsync<TConfig>(string configName) where TConfig : class
    {
        Serialize<TConfig>(configName, null);
    }

    /// <summary>
    /// Config will be intialized with a default instance given by {config} and saved at "./Configs/{configName}"
    /// </summary>
    /// <typeparam name="TConfig"></typeparam>
    /// <param name="configName"></param>
    public async void RegistConfigAsync<TConfig>(string configName, TConfig config) where TConfig : class
    {
        Serialize<TConfig>(configName, config);
    }
    #endregion

    #region Initialize
    private void Initialize()
    {
        var currentPath = Directory.GetCurrentDirectory();
        _configPath = Path.Combine(currentPath, PathConst.DEFAULT_CONFIG_PATH);
    }
    #endregion

    #region Deserialize
    private TConfig? Deserialize<TConfig>(string configName) where TConfig : class
    {
        if (Directory.Exists(_configPath))
        {
            var fileName = Path.Combine(_configPath, configName);
            if (File.Exists(fileName))
            {
                var jsonContent = File.ReadAllText(fileName);
                var result = JsonConvert.DeserializeObject<TConfig>(jsonContent);
                return result;
            }
        }
        return null;
    }

    private async Task<TConfig?> DeserializeAsync<TConfig>(string configName) where TConfig : class
    {
        if (Directory.Exists(_configPath))
        {
            var fileName = Path.Combine(_configPath, configName);
            if (File.Exists(fileName))
            {
                var jsonContent = await File.ReadAllTextAsync(fileName);
                var result = JsonConvert.DeserializeObject<TConfig>(jsonContent);
                return result;
            }
        }
        return null;
    }
    #endregion

    #region Serialize
    private void Serialize<TConfig>(string configName, TConfig? config) where TConfig : class
    {
        if (!Directory.Exists(_configPath))
        {
            Directory.CreateDirectory(_configPath);
        }
        // 如果config为空，则尝试调用public构造
        if (config == null)
        {
            var type = typeof(TConfig);
            var constructor = type.GetConstructor(
                BindingFlags.Instance | BindingFlags.Public,
                null,
                Type.EmptyTypes,
                null);
            // 如果无法构造，则直接返回
            if (constructor != null)
            {
                config = (TConfig)constructor.Invoke(null);
                _configs[configName] = config;
            }
            else
            {
                return;
            }
        }
        // 如果config不为空，则直接序列化
        var json = JsonConvert.SerializeObject(config);
        File.WriteAllText(configName, json);
    }

    private async void SerializeAsync<TConfig>(string configName, TConfig? config) where TConfig : class
    {
        if (!Directory.Exists(_configPath))
        {
            Directory.CreateDirectory(_configPath);
        }
        // 如果config为空，则尝试调用public构造
        if (config == null)
        {
            var type = typeof(TConfig);
            var constructor = type.GetConstructor(
                BindingFlags.Instance | BindingFlags.Public,
                null,
                Type.EmptyTypes,
                null);
            // 如果无法构造，则直接返回
            if (constructor != null)
            {
                config = (TConfig)constructor.Invoke(null);
                _configs[configName] = config;
            }
            else
            {
                return;
            }
        }
        // 如果config不为空，则直接序列化
        var json = JsonConvert.SerializeObject(config);
        await File.WriteAllTextAsync(configName, json);
    }
    #endregion
}
