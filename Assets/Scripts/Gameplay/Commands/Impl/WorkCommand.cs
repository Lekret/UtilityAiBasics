using System.Collections;
using Gameplay.Ai;
using Gameplay.Objectives;
using Gameplay.Stats.Impl;
using UnityEngine;

namespace Gameplay.Commands.Impl
{
    public class WorkCommand : ICommand
    {
        private readonly AiController _target;
        private Coroutine _routine;

        public WorkCommand(AiController target)
        {
            _target = target;
        }

        public void Execute()
        {
            _routine = _target.StartCoroutine(Work());
        }
        
        public void Interrupt()
        {
            if (_routine != null)
            {
                _target.StopCoroutine(_routine);
                _routine = null;
            }
        }
        
        private IEnumerator Work()
        {
            var workSource = Object.FindObjectOfType<WorkSource>();
            var woodTransform = workSource.transform;
            
            _target.NavMeshAgent.SetDestination(woodTransform.position);
            yield return null;
            
            while (_target.NavMeshAgent.remainingDistance > Vector3.kEpsilon)
            {
                yield return null;
            }

            while (true)
            {
                yield return new WaitForSeconds(1);
                
                if (_target.Stats.TryGet(out Money money))
                {
                    money.Change(workSource.MoneyPerSec);
                }
            }
        }
    }
}