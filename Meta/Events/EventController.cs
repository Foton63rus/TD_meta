using System;
using System.Collections.Generic;

namespace TowerDefence
{
    public static class EventController// Код этого класса честно позаимствован у Pixeye
    {
        internal static Dictionary<int, List<IReceive>> signals = new Dictionary<int, List<IReceive>>();

        internal static void Invoke<T>(in T val)
        {
            if (!signals.TryGetValue(typeof(T).GetHashCode(), out var receiveList))
                return;
            int count = receiveList.Count;
            for (int index = count - 1; index >= 0; index--)
                (receiveList[index] as IReceive<T>).HandleSignal(in val);
            
            //for (int index = 0; index < count; ++index)
            //    (receiveList[index] as IReceive<T>).HandleSignal(in val);
        }

        internal static void Add(object obj)
        {
            var      interfaces = obj.GetType().GetInterfaces();
            IReceive receive    = obj as IReceive;
            foreach (Type type in interfaces)
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IReceive<>))
                {
                    Add(receive, type.GetGenericArguments()[0]);
                }
            }
        }

        internal static void Remove(object obj)
        {
            var interfaces = obj.GetType().GetInterfaces();
            var receive    = obj as IReceive;
            foreach (var type in interfaces)
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IReceive<>))
                    Remove(receive, type.GetGenericArguments()[0]);
            }
        }

        static void Add(IReceive receive, Type type)
        {
            int hashCode = type.GetHashCode();
            if (signals.TryGetValue(hashCode, out var receiveList))
                receiveList.Add(receive);
            else
                signals.Add(hashCode, new List<IReceive>()
                {
                    receive
                });
        }

        static void Remove(IReceive receive, Type type)
        {
            if (!signals.TryGetValue(type.GetHashCode(), out var receiveList))
                return;
            receiveList.Remove(receive);
        }

        static void OnDispose()
        {
            signals.Clear();
        }
    }
    //Интерфейс, который должен наследовать класс, желающий получать сообщения
    //Стоит заметить, что экземпляр класса надо кидать в EventController.Add(this)
    public interface IReceive<T> : IReceive
    {
        //обработка сообщения
        public void HandleSignal(in T arg);
    }
    public interface IReceive{ }
    
}
