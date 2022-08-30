using UnityEngine;

namespace TowerDefence
{
    public class PlayerCardCommandConfigurator
    {    // класс для формирования команд контроллеру
        private ICardCommand command;
        
        public PlayerCardCommandConfigurator( )
        {    //INIT
            refresh();
        }

        public void refresh()
        {
            command  = null;
        }

        public void Execute(int arg = 0)
        {   
            if (command == null)
            {
                if (arg < 0)
                {
                    return;
                }
                else
                {
                    Debug.Log("не выбрано действий");
                    return;
                }
            }
            else
            {
                command.Execute(arg);
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

        public void Execute(int arg)
        {
            controller.addCardToCurrentDeck( arg );
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

        public void Execute(int arg)
        {
            controller.removeCardFromCurrentDeck( arg );
        }

        public void Undo()
        {
            
        }

        public string ToString()
        {
            return "COMRemoveCardFromCurrentDeck";
        }
    }

    public class COMUpgradeCard : ICardCommand
    {
        private PlayerCardController controller;

        public COMUpgradeCard(PlayerCardController controller)
        {
            this.controller = controller;
        }
        public void Execute(int arg)
        {
            controller.upgradeCard(arg);
        }

        public void Undo()
        {
            
        }
        
        public override string ToString()
        {
            return "COMUpgradeCard";
        }
    }
    
    public class COMNextDeck : ICardCommand
    {
        private PlayerCardController controller;

        public COMNextDeck(PlayerCardController controller)
        {
            this.controller = controller;
        }

        public void Execute(int arg)
        {
            controller.nextDeck( );
            controller.commandConfigurator.refresh();
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
        void Execute(int arg);
        void Undo();
        string ToString();
    }
}
