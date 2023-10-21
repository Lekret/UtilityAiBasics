using Gameplay.Stats.Impl;
using UnityEngine;

namespace Gameplay.Ai.DecisionMaking.Considerations
{
    [CreateAssetMenu(menuName = "StaticData/Considerations/Hunger", fileName = "HungerConsideration")]
    public class SatietyConsideration : AiConsideration
    {
        public AnimationCurve ResponseCurve;
        
        public override float EvaluateScore(IAiCharacter target)
        {
            var score = 0f;
            
            if (target.Stats.TryGet(out Satiety satiety))
            {
                score = ResponseCurve.Evaluate((float) satiety.Value / satiety.MaxValue);
            }

            return score;
        }
    }
}