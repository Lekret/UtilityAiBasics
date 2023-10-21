using System.Collections;
using Gameplay.Ai;
using Gameplay.Objectives;
using Gameplay.Stats.Impl;
using UnityEngine;

namespace Gameplay.Commands.Impl
{
    public class RestCommand : ICommand
    {
        private readonly AiController _target;
        private Coroutine _restRoutine;

        public RestCommand(AiController target)
        {
            _target = target;
        }

        public void Execute()
        {
            _restRoutine = _target.StartCoroutine(Rest());
        }
        
        public void Interrupt()
        {
            if (_restRoutine != null)
            {
                _target.StopCoroutine(_restRoutine);
                _restRoutine = null;
            }
        }
        
        private IEnumerator Rest()
        {
            var bed = Object.FindObjectOfType<Bed>();
            var bedTransform = bed.transform;
            
            _target.NavMeshAgent.SetDestination(bedTransform.position);
            yield return null;
            
            while (_target.NavMeshAgent.remainingDistance > Vector3.kEpsilon)
            {
                yield return null;
            }

            while (true)
            {
                yield return new WaitForSeconds(1);
                
                if (_target.Stats.TryGet(out Energy energy))
                {
                    energy.Change(bed.EnergyPerSec);
                }
            }
        }
    }
}