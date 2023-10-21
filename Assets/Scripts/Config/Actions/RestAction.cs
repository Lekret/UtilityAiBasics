using Gameplay.Commands.Impl;
using UnityEngine;

namespace Gameplay.Ai.DecisionMaking.Actions
{
    [CreateAssetMenu(menuName = "StaticData/Actions/Sleep", fileName = "SleepAction")]
    public class RestAction : AiAction
    {
        public override void Execute(IAiCharacter target)
        {
            target.CommandExecutor.ChangeCommand<RestCommand>();
        }
    }
}