using Gameplay;
using Gameplay.Ai;
using Gameplay.Ai.DecisionMaking;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(menuName = "StaticData/AiBrainConfig", fileName = "AiBrainConfig")]
    public class AiBrainConfig : ScriptableObject
    {
        public AiAction[] Actions;
    }
}