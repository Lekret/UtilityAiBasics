using Gameplay.Commands.Impl;
using UnityEngine;

namespace Gameplay.Ai.DecisionMaking.Actions
{
    [CreateAssetMenu(menuName = "StaticData/Actions/Work", fileName = "WorkAction")]
    public class WorkAction : AiAction
    {
        public override void Execute(IAiCharacter target)
        {
            target.CommandExecutor.ChangeCommand<WorkCommand>();
        }
    }
}