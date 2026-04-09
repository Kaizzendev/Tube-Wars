using UnityEngine;

namespace AIUtility
{
    public abstract class UtilitySystemEvaluationFunction: UtilitySystemEvaluation
    {
        public UtilitySystemEvaluation evaluationFunction;
        public abstract override float GetScore(IAContext context);
    }
}