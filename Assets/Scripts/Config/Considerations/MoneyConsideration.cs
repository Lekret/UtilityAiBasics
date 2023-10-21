using Gameplay.Stats.Impl;
using UnityEngine;

namespace Gameplay.Ai.DecisionMaking.Considerations
{
    [CreateAssetMenu(menuName = "StaticData/Considerations/Money", fileName = "MoneyConsideration")]
    public class MoneyConsideration : AiConsideration
    {
        public int MaxMoney;
        public AnimationCurve ResponseCurve;

        public override float EvaluateScore(IAiCharacter target)
        {
            var score = 0f;
            
            if (target.Stats.TryGet(out Money money))
            {
                score = ResponseCurve.Evaluate((float) money.Value / MaxMoney);
            }

            return score;
        }
    }
}