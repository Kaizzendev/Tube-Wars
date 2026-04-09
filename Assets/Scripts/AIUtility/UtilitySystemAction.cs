using UnityEngine;

public abstract class UtilitySystemAction
{
    internal UtilitySystemEvaluation _utilitySystemEvaluation;

    public float EvaluateScore()
    {
        return _utilitySystemEvaluation.GetScore();
    }
    public abstract void ExecuteAction();
}
