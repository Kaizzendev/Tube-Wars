using System;
using UnityEngine;

namespace AIUtility
{
    [CreateAssetMenu(fileName="FunctionEvaluation", menuName = "Utility System/Evaluation/Function/Strength")]
    public class UtilitySystemEvaluationRelativeStrength: UtilitySystemEvaluationFunction
    {
        public AnimationCurve curve;
        public override float GetScore()
        {
            float inputScore = //TODO recibir info del contexto;
            float utilityScore = curve.Evaluate(inputScore);
            return Mathf.Clamp01(utilityScore);
        }
    }
}