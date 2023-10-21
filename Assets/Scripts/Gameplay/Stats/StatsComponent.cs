using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Stats
{
    public class StatsComponent : MonoBehaviour
    {
        private readonly List<Stat> _stats = new();

        public IReadOnlyList<Stat> Stats => _stats;

        public void AddStat(Stat stat)
        {
            _stats.Add(stat);
        }

        public bool TryGet<T>(out T stat) where T : Stat
        {
            stat = Get<T>();
            return stat != null;
        }
        
        public T Get<T>() where T : Stat
        {
            return (T) _stats.Find(s => s is T);
        }
    }
}