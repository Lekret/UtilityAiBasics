using System.Collections;
using Gameplay.Ai;
using Gameplay.Objectives;
using Gameplay.Stats.Impl;
using UnityEngine;

namespace Gameplay.Commands.Impl
{
    public class EatCommand : ICommand
    {
        private readonly AiController _target;
        private Coroutine _routine;
        
        public EatCommand(AiController target)
        {
            _target = target;
        }

        public void Execute()
        {
            _routine = _target.StartCoroutine(Eat());
        }

        public void Interrupt()
        {
            if (_routine != null)
            {
                _target.StopCoroutine(_routine);
                _routine = null;
            }
        }
        
        private IEnumerator Eat()
        {
            var foodSource = Object.FindObjectOfType<FoodSource>();
            var foodTransform = foodSource.transform;
            
            _target.NavMeshAgent.SetDestination(foodTransform.position);
            yield return null;
            
            while (_target.NavMeshAgent.remainingDistance > Vector3.kEpsilon)
            {
                yield return null;
            }

            while (true)
            {
                yield return new WaitForSeconds(1);
                
                if (_target.Stats.TryGet(out Satiety satiety))
                {
                    satiety.Change(foodSource.SatietyPerSec);
                }
            }
        }
    }
}