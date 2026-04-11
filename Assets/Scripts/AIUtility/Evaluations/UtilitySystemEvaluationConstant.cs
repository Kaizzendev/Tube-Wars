using UnityEngine;

namespace AIUtility
{
    [CreateAssetMenu(fileName="FunctionEvaluation", menuName = "Utility System/Evaluation/Function/Constant")]
    public class UtilitySystemEvaluationConstant: UtilitySystemEvaluation
    {
        [SerializeField] private float constant;
        public override float GetScore(IAContext context)
        {
            return constant;
        }
    }
}