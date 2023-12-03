using System.Collections.Generic;

#if DEBUG_LOG
using UnityEngine;
#endif

namespace RFW.Events
{
    public static class EventBus<T> where T : IEvent
    {
        static readonly HashSet<IEventBinding<T>> bindings = new HashSet<IEventBinding<T>>();

        public static void Register(EventBinding<T> binding) => bindings.Add(binding);
        public static void Unregister(EventBinding<T> binding) => bindings.Remove(binding);

        public static bool HasBinding() => bindings.Count > 0;

        public static void Raise(T @event)
        {
            foreach (var binding in bindings)
            {
                binding.OnEvent.Invoke(@event);
                binding.OnEventNoArgs.Invoke();
            }
        }

        static void Clear()
        {
#if DEBUG_LOG
            Debug.Log($"Clearing {typeof(T).Name} bindings");
#endif
            bindings.Clear();
        }
    }
}