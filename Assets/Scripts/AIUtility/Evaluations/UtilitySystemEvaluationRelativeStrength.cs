using System;
using UnityEngine;

namespace AIUtility
{
    [CreateAssetMenu(fileName="FunctionEvaluation", menuName = "Utility System/Evaluation/Function/Strength")]
    public class UtilitySystemEvaluationRelativeStrength: UtilitySystemEvaluationFunction
    {
        public override float GetScore(IAContext context)
        {
            float inputScore = (float) context.self.totalUnits / context.totalUnits;
            Debug.Log("Unidades del equipo: "+ context.self.totalUnits +" / Unidades totales" + context.totalUnits + " / Input score: " + inputScore);
            float utilityScore = curve.Evaluate(inputScore);
            return Mathf.Clamp01(utilityScore);
        }
    }
}