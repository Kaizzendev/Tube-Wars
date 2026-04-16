using System;
using UnityEngine;

public abstract class UtilitySystemAction: ScriptableObject
{
    public UtilitySystemEvaluation utilitySystemEvaluation;
    public String actionName;
    public float EvaluateScore(IAContext context)
    {
        return utilitySystemEvaluation.GetScore(context);
    }
    public abstract void ExecuteAction(IAContext context);
}
