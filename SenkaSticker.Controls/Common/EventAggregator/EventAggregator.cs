using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenkaSticker.Controls.Common.EventAggregator
{
    public static class EventAggregator
    {
        #region Fields
        private static ConcurrentDictionary<Type, List<Action<IEvent>>> _eventHandlers = new();
        #endregion

        #region Methods
        public static void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : IEvent
        {
            var eventType = typeof(TEvent);
            if (!_eventHandlers.ContainsKey(eventType))
            {
                _eventHandlers[eventType] = new();
            }
            _eventHandlers[eventType].Add(handler as Action<IEvent>);
        }

        public static void Publish(IEvent @event)
        {
            var eventType = @event.GetType();
            if (_eventHandlers.ContainsKey(eventType))
            {
                foreach (var handler in _eventHandlers[eventType])
                {
                    handler(@event);
                }
            }
        }

        public static Task PublishAsync(IEvent @event)
        {
            return Task.Run(() => Publish(@event));
        }
        #endregion
    }
}
