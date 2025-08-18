using Avalonia.Controls.ApplicationLifetimes;
using Caliburn.Micro;
using SenkaSticker.ViewModels;
using System.Collections.Generic;
using System;

namespace SenkaSticker
{
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
                .Singleton<IEventAggregator, EventAggregator>();

            _container
               .PerRequest<MainViewModel>();
        }

        protected override async void OnStartup(object sender, ControlledApplicationLifetimeStartupEventArgs e)
        {

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
        #endregion
    }
}
