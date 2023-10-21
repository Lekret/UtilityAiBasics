namespace Gameplay.Commands
{
    public interface ICommand
    {
        void Execute();
        void Interrupt();
    }
}