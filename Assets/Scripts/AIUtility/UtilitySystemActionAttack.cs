using System.Collections.Generic;
using UnityEngine;

namespace AIUtility
{
    [CreateAssetMenu(fileName="IAAction", menuName = "Utility System/Actions/Attack")]  
    public class UtilitySystemActionAttack : UtilitySystemAction
    {
        PathFinding _pathFinding;

        public override void ExecuteAction(IAContext context)
        {
               //TODO: Mandar unidades a un nodo
               Debug.Log("Ataco: " + utilitySystemEvaluation.name);
        }
        
    }
}