using System.Collections.Generic;
using UnityEngine;

namespace AIUtility
{
    [CreateAssetMenu(fileName="IAAction", menuName = "Utility System/Actions/Attack")]  
    public class UtilitySystemActionAttack : UtilitySystemAction
    {
        private Node.Node _targetNode;
        private Node.Node _originNode;
        public override void ExecuteAction(IAContext context)
        {
            float score = 0;
            foreach (Node.Node allyNode in context.self.ownedNodes)
            {
               foreach (Node.Node node in allyNode.neighbours)
               {
                   if (node.ownerId == context.self.id) continue;

                   float newScore = CheckBestOption(allyNode, node);
                   if (score < newScore)
                   {
                       _targetNode = node;
                       _originNode = allyNode;
                       score = newScore;
                   }
               }
            }

            context.combatSystem.SendUnits(_originNode, _targetNode);
        }

        private float CheckBestOption(Node.Node origin, Node.Node target)
        {
            float denominator = Mathf.Max(target.currentUnits, 1f);
            return origin.currentUnits / denominator;
        }
        
    }
}