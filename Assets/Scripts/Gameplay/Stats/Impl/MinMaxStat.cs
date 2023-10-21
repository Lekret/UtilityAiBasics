using UnityEngine;

namespace Gameplay.Stats.Impl
{
    public class MinMaxStat : Stat
    {
        public MinMaxStat(int value, int maxValue)
        {
            Value = value;
            MaxValue = maxValue;
        }

        public int Value { get; private set; }
        public int MaxValue { get; }

        public void Change(int delta)
        {
            Value = Mathf.Clamp(Value + delta, 0, MaxValue);
        }
    }
}