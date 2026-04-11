using System.Collections.Generic;
using UnityEngine;

namespace AIUtility
{
    public abstract class UtilitySystemEvaluationFusion: UtilitySystemEvaluation 
    {
        public List<UtilitySystemEvaluation> evaluations;

        public abstract override float GetScore(IAContext context);

        private float FusionSum(float a, float b)
        {
            return a + b;
        }

        private float FusionSustraction(float a, float b)
        {
            return a - b;
        }

        private float FusionMultiply(float a, float b)
        {
            return a * b;
        }

        private float FusionDivision(float a, float b)
        {
            return a / b;
        }
        
    }
}