global using ILogger = log4net.ILog;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace SenkaSticker.Common.Logger
{
    public static class LoggerFactory
    {
        #region Constructor
        #endregion

        #region Events
        #endregion

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Methods
        public static ILogger GetLogger(string name)
        {
            // 获取当前程序集
            Assembly assembly = Assembly.GetExecutingAssembly();
            // 从指定程序集的日志仓库中获取logger
            ILogger logger = (ILogger)LogManager.GetLogger(assembly, name);
            return logger;
        }

        public static void Configure()
        {
            var hierarchy = (Hierarchy)LogManager.GetRepository();

            var consolePatternLayout = new PatternLayout("[%date{yyyy-MM-dd HH:mm:ss:fff}][%-30c][%-10thread][%-10level]%message%newline");
            consolePatternLayout.ActivateOptions();
            var consoleAppender = new ConsoleAppender()
            {
                Layout = consolePatternLayout,
            };
            consoleAppender.ActivateOptions();

            var filePatternLayout = new PatternLayout("[%date{yyyy-MM-dd HH:mm:ss:fff}][%-30c][%-10thread][%-10level]%message%newline");
            filePatternLayout.ActivateOptions();
            var rollingFileAppender = new RollingFileAppender()
            {
                File = "Logs\\Simulator.log",
                AppendToFile = true,
                RollingStyle = RollingFileAppender.RollingMode.Composite,
                DatePattern = "yyyyMMdd",
                MaxSizeRollBackups = 10,
                StaticLogFileName = false,
                CountDirection = 1,
                PreserveLogFileNameExtension = true,
                Layout = filePatternLayout,
            };
            rollingFileAppender.ActivateOptions();
            hierarchy.Root.RemoveAllAppenders();
            hierarchy.Root.AddAppender(consoleAppender);
            hierarchy.Root.AddAppender(rollingFileAppender);
            hierarchy.Root.Level = Level.All;
            hierarchy.Configured = true;
        }

        public static void DebugIn(this ILogger logger, [CallerMemberName] string callMember = "")
        {
            logger.Debug($"Enter [{callMember}]");
        }

        public static void DebugOut(this ILogger logger, [CallerMemberName] string callMember = "")
        {
            logger.Debug($"Leave [{callMember}]");
        }

        public static void DebugIn(this ILogger logger, [CallerMemberName] string callMember = "", params object[] args)
        {
            var argsString = GetArgsString(args);
            logger.Debug($"Enter [{callMember}] with args:{argsString}");
        }

        public static void DebugOut(this ILogger logger, [CallerMemberName] string callMember = "", params object[] args)
        {
            var argsString = GetArgsString(args);
            logger.Debug($"Leave [{callMember}] with args:{argsString}");
        }


        private static string GetArgsString(object[] args, [CallerArgumentExpression("args")] string argExpressions = "")
        {
            var expressions = ParseExpressions(argExpressions);

            if (args != null && args.Count() > 0)
            {
                var stringBuilder = new StringBuilder();

                var arg = expressions[0].Trim();
                stringBuilder.Append(args.Count() == 1 ? $"[{arg}]= [{ args[0]}]" : $"[{arg}]= [{args[0]}],");

                for (int i = 1; i < args.Length; i++)
                {
                    arg = expressions[i].Trim();
                    expressions.Append($"[{arg}]=[{args[i]}]");
                }

                return stringBuilder.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        private static string[] ParseExpressions(string expressions)
        {
            if (expressions.StartsWith("{") && expressions.EndsWith("}"))
            {
                expressions = expressions.Substring(1, expressions.Length - 2);
            }
            return expressions.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }
        #endregion
    }
}
