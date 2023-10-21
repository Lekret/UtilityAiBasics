using Gameplay.Stats.Impl;
using UnityEngine;

namespace Gameplay.Ai.DecisionMaking.Considerations
{
    [CreateAssetMenu(menuName = "StaticData/Considerations/Energy", fileName = "EnergyConsideration")]
    public class EnergyConsideration : AiConsideration
    {
        public AnimationCurve ResponseCurve;
        
        public override float EvaluateScore(IAiCharacter target)
        {
            var score = 0f;
            
            if (target.Stats.TryGet(out Energy energy))
            {
                score = ResponseCurve.Evaluate((float) energy.Value / energy.MaxValue);
            }

            return score;
        }
    }
}