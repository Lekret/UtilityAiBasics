using Gameplay.Commands.Impl;
using UnityEngine;

namespace Gameplay.Ai.DecisionMaking.Actions
{
    [CreateAssetMenu(menuName = "StaticData/Actions/Eat", fileName = "EatAction")]
    public class EatAction : AiAction
    {
        public override void Execute(IAiCharacter target)
        {
            target.CommandExecutor.ChangeCommand<EatCommand>();
        }
    }
}