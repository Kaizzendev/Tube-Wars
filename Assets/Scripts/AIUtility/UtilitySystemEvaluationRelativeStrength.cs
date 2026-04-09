using System;
using UnityEngine;

namespace AIUtility
{
    [CreateAssetMenu(fileName="FunctionEvaluation", menuName = "Utility System/Evaluation/Function/Strength")]
    public class UtilitySystemEvaluationRelativeStrength: UtilitySystemEvaluationFunction
    {
        public AnimationCurve curve;
        public override float GetScore(IAContext context)
        {
            float inputScore = context.self.totalUnits;
            float utilityScore = curve.Evaluate(inputScore);
            return Mathf.Clamp01(utilityScore);
        }
    }
}