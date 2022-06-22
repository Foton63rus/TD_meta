using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    [Serializable] //Монобех, который вешается на Мету и хранит ссылки на контроллеры
    public class ControllerContainer : MonoBehaviour
    {    
        [SerializeField] // Список контроллеров
        private List<Controller> _controllers;

        public Dictionary<string, Controller> controllers;

        //Инициализация всех контроллов
        public void Init(Meta meta)
        {
            controllers = new Dictionary<string, Controller>();
            if (_controllers.Count > 0)
            {
                foreach (Controller controller in _controllers)
                {
                    controllers[typeof(Controller).Name] = controller;
                    Debug.Log(controller.GetType().ToString());
                    controller.Init( meta);
                }
            }
        }
    }
    
    public interface IController
    {//Все контроллы должны принимать Мету при инициализации
        public void Init( Meta meta);
    }

    public abstract class Controller : MonoBehaviour, IController
    {
        public virtual void Init(Meta meta)
        {   //Так как в этом классе реализована интерфейс IController, то нужно в каждом классе оверрайдить метод Инит
            throw new Exception("Нужно добавить override для метода Init в контроллере");
        }
    }
}
