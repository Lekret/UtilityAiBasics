using UnityEngine;

namespace Gameplay.Ai.DecisionMaking
{
    public abstract class AiConsideration : ScriptableObject, IUtilityConsideration<IAiCharacter>
    {
        public virtual void Init() { }

        public abstract float EvaluateScore(IAiCharacter target);
    }
}