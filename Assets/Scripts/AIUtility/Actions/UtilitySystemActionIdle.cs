using UnityEngine;

namespace AIUtility
{
    [CreateAssetMenu(fileName="IAAction", menuName = "Utility System/Actions/Idle")]  
    public class UtilitySystemActionIdle: UtilitySystemAction
    {
        public override void ExecuteAction(IAContext context)
        {
           Debug.Log("Idle");
        }
    }
}