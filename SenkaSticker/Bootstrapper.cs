using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Platform;
using Caliburn.Micro;
using SenkaSticker.Common.Consts;
using SenkaSticker.Common.CustomAssembly;
using SenkaSticker.Common.Logger;
using SenkaSticker.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using System.Threading.Tasks;

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
    private TrayIcon _trayIcon;
    private Window _mainWindow;
    #endregion

    #region Methods
    protected override void Configure()
    {
        _container
            .Singleton<IWindowManager, WindowManager>()
            .Singleton<IEventAggregator, EventAggregator>()
            .Singleton<MainViewModel>();

        LoadAssembly();
        LoggerFactory.Configure();
    }

    protected override async void OnStartup(object sender, ControlledApplicationLifetimeStartupEventArgs e)
    {
        await DisplayRootViewFor<MainViewModel>();
        InitializeTrayIcon();
    }

    protected override void OnExit(object sender, ControlledApplicationLifetimeExitEventArgs e)
    {
        _trayIcon?.Dispose();
        base.OnExit(sender, e);
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

    private void InitializeTrayIcon()
    {
        _trayIcon = new TrayIcon()
        {
            Icon = new WindowIcon(AssetLoader.Open(new Uri("avares://SenkaSticker/Assets/logo.ico"))),
            ToolTipText = "Display Senka Sticker"
        };
        if (Application.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop && desktop.MainWindow != null)
        {
            _mainWindow = desktop.MainWindow;
            _mainWindow.PropertyChanged += OnMainWindowPropertyChanged;
        }
        _trayIcon.Clicked += OnTrayIconClicked;
    }

    private void OnMainWindowPropertyChanged(object? sender, Avalonia.AvaloniaPropertyChangedEventArgs arg)
    {
        if (arg.Property == Window.WindowStateProperty)
        {
            OnWindowStateChanged((WindowState)arg.NewValue);
        }
    }

    private void OnTrayIconClicked(object? sender, EventArgs e)
    {
        _mainWindow.Show();
        _mainWindow.SetValue<WindowState>(Window.WindowStateProperty, WindowState.Normal);
    }

    private void OnWindowStateChanged(WindowState newState)
    {
        if (newState == WindowState.Minimized)
        {
            _mainWindow.Hide();
        }
    }
    #endregion
}