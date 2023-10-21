using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Commands
{
    public class CommandExecutor : MonoBehaviour
    {
        private readonly List<ICommand> _commands = new();

        public ICommand CurrentCommand { get; private set; }

        public void AddCommand(ICommand command)
        {
            _commands.Add(command);
        }

        public void ChangeCommand<T>() where T : ICommand
        {
            if (CurrentCommand != null)
                CurrentCommand.Interrupt();
            
            CurrentCommand = _commands.Find(c => c is T);
            
            if (CurrentCommand == null)
                Debug.LogError($"Command of type {typeof(T)} not found");
            else
                CurrentCommand.Execute();
        }
    }
}