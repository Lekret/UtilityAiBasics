using System.Collections.Generic;
using UnityEngine;

public interface IUtilityAction<TContext, TConsideration> where TConsideration : IUtilityConsideration<TContext>
{
    public IReadOnlyList<TConsideration> Considerations { get; }
}

public interface IUtilityConsideration<TContext>
{
    float EvaluateScore(TContext context);
}

public static class UtilityAi
{
    public static TAction FindBestAction<TContext, TAction, TConsideration>(
        IReadOnlyList<TAction> actions, TContext context) 
        where TAction : IUtilityAction<TContext, TConsideration>
        where TConsideration : IUtilityConsideration<TContext>
    {
        var actionsCount = actions.Count;
        if (actionsCount == 0)
            return default;
            
        var bestScore = 0f;
        var bestActionIdx = 0;
        for (var i = 0; i < actionsCount; i++)
        {
            var newScore = EvaluateActionScore<TContext, TAction, TConsideration>(actions[i], context);
            if (newScore > bestScore)
            {
                bestActionIdx = i;
                bestScore = newScore;
            }
        }
            
        return actions[bestActionIdx];
    }
    
    public static float EvaluateActionScore<TContext, TAction, TConsideration>(TAction action, TContext context) 
        where TAction : IUtilityAction<TContext, TConsideration>
        where TConsideration : IUtilityConsideration<TContext>
    {
        var considerations = action.Considerations;
        var considerationsCount = considerations.Count;
        if (considerationsCount == 0)
            return 0f;
            
        var score = 1f;
        for (var i = 0; i < considerationsCount; i++)
        {
            var considerationScore = Mathf.Clamp01(considerations[i].EvaluateScore(context));
            score *= considerationScore;
            if (score == 0f)
                return score;
        }

        var modFactor = 1f - 1f / considerationsCount;
        var makeupValue = (1f - score) * modFactor;
        score += score * makeupValue;
        return score;
    }
}