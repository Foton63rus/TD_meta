using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TowerDefence
{
    public class PlayerCardCommandConfigurator
    {    // класс для формирования команд контроллеру
        private PlayerCardController receiver;
        private List<int> arguments = new List<int>();
        private ICardCommand command;
        
        public PlayerCardCommandConfigurator( PlayerCardController receiver)
        {    //INIT
            this.receiver = receiver;
            refresh();
        }

        private void refresh()
        {
            command = null;
            arguments.Clear();
        }

        public void Execute(int[] args)
        {
            if (command == null)
            {
                if (args == null || args.Length == 0)
                {
                    return;
                }
                else
                {
                    arguments.Clear();
                    arguments.AddRange(args);
                }
                
            }
            else
            {
                command.Execute(args);
            }
        }

        public void addCommand(ICardCommand newCommand)
        {
            if (newCommand != null)
            {
                command = newCommand;
                Debug.Log($"type NewCommand {newCommand.ToString()}");
            }
        }

        public void switchCommand(ICardCommand newCommand)
        {    //toggle
            if (command == null)
            {
                addCommand(newCommand);
            }
            else
            {
                if ( newCommand.ToString() == command.ToString() )
                {
                    Debug.Log($"cancel command {command}");
                    command = null;
                }
                else
                {
                    addCommand(newCommand);
                }
            }
        }
    }

    public class COMAddCardToCurrentDeck : ICardCommand
    {
        private PlayerCardController controller;

        public COMAddCardToCurrentDeck(PlayerCardController controller)
        {
            this.controller = controller;
        }

        public void Execute(int[] args)
        {
            controller.addCardToCurrentDeck( args[0] );
        }

        public void Undo()
        {
            
        }

        public string ToString()
        {
            return "COMAddCardToCurrentDeck";
        }
    }
    
    public class COMRemoveCardFromCurrentDeck : ICardCommand
    {
        private PlayerCardController controller;

        public COMRemoveCardFromCurrentDeck(PlayerCardController controller)
        {
            this.controller = controller;
        }

        public void Execute(int[] args)
        {
            controller.removeCardFromCurrentDeck( args[0] );
        }

        public void Undo()
        {
            
        }

        public string ToString()
        {
            return "COMRemoveCardFromCurrentDeck";
        }
    }
    
    public class COMNextDeck : ICardCommand
    {
        private PlayerCardController controller;

        public COMNextDeck(PlayerCardController controller)
        {
            this.controller = controller;
        }

        public void Execute(int[] args = null)
        {
            controller.nextDeck( );
        }

        public void Undo()
        {
            
        }

        public override string ToString()
        {
            return "COMNextDeck";
        }
    }
    
    public interface ICardCommand
    {
        void Execute(int[] args = null);
        void Undo();
        string ToString();
    }
}
