using UnityEngine;

namespace Gameplay.Stats.Impl
{
    public class ValueStat : Stat
    {
        public ValueStat(int value)
        {
            Value = value;
        }

        public int Value { get; private set; }

        public void Change(int delta)
        {
            Value = Mathf.Max(Value + delta, 0);
        }
    }
}