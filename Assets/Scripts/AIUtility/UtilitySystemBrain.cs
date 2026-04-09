using System.Collections.Generic;
using UnityEngine;

namespace AIUtility
{
    public class UtilitySystemBrain : MonoBehaviour
    {
        private float _bestScore = 0;
        private UtilitySystemAction _bestAction = null;
        private List<UtilitySystemAction> _actions = new List<UtilitySystemAction>();

        private void Sense()
        {
            // Gather world data
        }

        private void Think()
        {
            // Evaluate data
        }

        private void Act()
        {
            foreach (var action in _actions)
            {
                float score = action.EvaluateScore();

                if (score > _bestScore)
                {
                    _bestScore = score;
                    _bestAction = action;
                }

            }
        }
    }
}
