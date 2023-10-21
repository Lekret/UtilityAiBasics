using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Ai.DecisionMaking
{
    public abstract class AiAction : ScriptableObject, IUtilityAction<IAiCharacter, AiConsideration>
    {
        [SerializeField] private AiConsideration[] _considerations;

        public IReadOnlyList<AiConsideration> Considerations => _considerations;

        public virtual void Init() { }
        
        public abstract void Execute(IAiCharacter target);
    }
}