using System;
using Gameplay.Ai.DecisionMaking;
using Gameplay.Commands;
using Gameplay.Commands.Impl;
using Gameplay.Stats;
using Gameplay.Stats.Impl;
using StaticData;
using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace Gameplay.Ai
{
    [RequireComponent(typeof(StatsComponent))]
    [RequireComponent(typeof(CommandExecutor))]
    public class AiController : MonoBehaviour, IAiCharacter
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private AiBrainConfig _brainConfig;
        [SerializeField] private InitialStats _initialStats;

        private readonly IntervalTimer _updateBrainTimer = new(0.1f);
        private readonly IntervalTimer _updateStatsTimer = new(1);
        private AiAction _currentAction;

        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        public StatsComponent Stats { get; private set; }
        public CommandExecutor CommandExecutor { get; private set; }

        private void Awake()
        {
            Stats = GetComponent<StatsComponent>();
            CommandExecutor = GetComponent<CommandExecutor>();
            InitStats();
            InitCommands();
            var actions = Array.ConvertAll(_brainConfig.Actions, Instantiate);
            InitializeActions(actions);
        }

        private void InitStats()
        {
            Stats.AddStat(new Energy(_initialStats.Energy, _initialStats.MaxEnergy));
            Stats.AddStat(new Satiety(_initialStats.Satiety, _initialStats.MaxSatiety));
            Stats.AddStat(new Money(_initialStats.Money));
        }

        private void InitCommands()
        {
            CommandExecutor.AddCommand(new RestCommand(this));
            CommandExecutor.AddCommand(new EatCommand(this));
            CommandExecutor.AddCommand(new WorkCommand(this));
        }

        private void InitializeActions(AiAction[] actions)
        {
            foreach (var act in actions)
            {
                foreach (var c in act.Considerations)
                {
                    c.Init();
                }
                act.Init();
            }
        }

        private void Update()
        {
            if (_updateBrainTimer.Tick(Time.deltaTime))
            {
                UpdateBrain();
            }

            if (_updateStatsTimer.Tick(Time.deltaTime))
            {
                UpdateStats();
            }
        }

        private void UpdateBrain()
        {
            var newBestAction = UtilityAi.FindBestAction<IAiCharacter, AiAction, AiConsideration>(
                _brainConfig.Actions, this);
            
            if (_currentAction == newBestAction)
                return;
 
            var currentActionName = _currentAction ? _currentAction.GetType().Name : "null";
            var nextActionName = newBestAction.GetType().Name;
            Debug.Log($"CurrentAction change: {currentActionName} -> {nextActionName}");
            
            _currentAction = newBestAction;
            _currentAction.Execute(this);
        }

        private void UpdateStats()
        {
            Stats.Get<Energy>().Change(-1);
            Stats.Get<Satiety>().Change(-1);
            Stats.Get<Money>().Change(-1);
        }
    }
}