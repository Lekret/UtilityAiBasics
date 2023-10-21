using Gameplay.Commands;
using Gameplay.Stats;

namespace Gameplay.Ai
{
    public interface IAiCharacter
    {
        public StatsComponent Stats { get; }
        public CommandExecutor CommandExecutor { get; }
    }
}