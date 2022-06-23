using System;
using UnityEngine;

namespace TowerDefence
{
    [Serializable] //Монобех, который вешается на Мету и хранит ссылку на объект с контроллерами
    public class ControllerContainer : MonoBehaviour
    {    
        [SerializeField] // GameObject, на который вешаем все контроллы
        private GameObject goControllers;

        //Инициализация всех контроллов
        public void Init(Meta meta)
        {
            Component[] components = goControllers.GetComponents(typeof(Controller));
            if (components.Length > 0)
            {
                foreach (Controller component in components)
                {
                    component.Init(meta);
                    //Debug.Log( $"name: {component.GetType().Name}" );
                }
            }
        }
    }
    
    public interface IController
    {//Все контроллы должны принимать Мету при инициализации
        public void Init( Meta meta);
    }

    public abstract class Controller : MonoBehaviour, IController
    {    //Все контроллеры нужно наследовать от этого класса
        public virtual void Init(Meta meta)
        {   //Так как в этом классе реализована интерфейс IController, то нужно в каждом классе оверрайдить метод Инит
            throw new Exception("Нужно добавить override для метода Init в контроллере");
        }
    }
}
