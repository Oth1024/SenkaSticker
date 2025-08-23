using Avalonia.Controls.ApplicationLifetimes;
using Caliburn.Micro;
using SenkaSticker.ViewModels;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.IO;
using SenkaSticker.Common.Consts;
using System.Linq;
using SenkaSticker.Common.CustomAssembly;

namespace SenkaSticker;

public class Bootstrapper : BootstrapperBase
{
    #region Constructor
    public Bootstrapper()
    {
        Initialize();
    }
    #endregion

    #region Fields
    private SimpleContainer _container = new SimpleContainer();
    #endregion

    #region Methods
    protected override void Configure()
    {
        _container
            .Singleton<IWindowManager, WindowManager>()
            .Singleton<IEventAggregator, EventAggregator>()
            .Singleton<MainViewModel>();

        LoadAssembly();
    }

    protected override async void OnStartup(object sender, ControlledApplicationLifetimeStartupEventArgs e)
    {
        await DisplayRootViewFor<MainViewModel>();
    }

    protected override object GetInstance(Type service, string key)
    {
        return _container.GetInstance(service, key);
    }

    protected override IEnumerable<object> GetAllInstances(Type service)
    {
        return _container.GetAllInstances(service);
    }

    protected override void BuildUp(object instance)
    {
        _container.BuildUp(instance);
    }

    private void LoadAssembly()
    {

        // Load current executing
        var assemblySource = AssemblySource.Instance;
        assemblySource.Add(Assembly.GetExecutingAssembly());
        var currentDir = Directory.GetCurrentDirectory();

        // Load from current path
        LoadAssemblyFromDir(ref assemblySource, currentDir, out _);

        // Load from gui
        var guiPath = Path.Combine(currentDir, PathConst.GUI_ASSEMBLY_PATH);
        LoadAssemblyFromDir(ref assemblySource, guiPath, out var guiAssemblies);
        CustomAssemblyManager.Instance.AddGuiAssemblies(guiAssemblies);

        // Load from extension
        var extensionPath = Path.Combine(currentDir, PathConst.EXTENSION_ASSEMBLY_PATH);
        LoadAssemblyFromDir(ref assemblySource, extensionPath, out var extensionAssembies);
        CustomAssemblyManager.Instance.AddExtensionAssemblies(extensionAssembies);
    }

    private void LoadAssemblyFromDir(ref IObservableCollection<Assembly> assemblySource, string directory, out List<Assembly> assemblies)
    {
        assemblies = new List<Assembly>();
        if (Directory.Exists(directory))
        {
            var files = Directory.GetFiles(directory);
            if (files != null && files.Any())
            {
                foreach (var file in files)
                {
                    try
                    {
                        if (file.EndsWith(".dll"))
                        {
                            var assembly = Assembly.LoadFrom(file);
                            assemblySource.Add(assembly);
                            assemblies.Add(assembly);
                        }
                    }
                    catch
                    {

                    }
                }
            }
        }
    }
    #endregion
}