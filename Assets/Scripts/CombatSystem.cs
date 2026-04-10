using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    public PathFinding pathFinding;
    public SquadManager squadManager;
    public void SendUnits(Node.Node origin, Node.Node target)
    {
        List<Node.Node> path = pathFinding.GetPath(origin, target);
        if (path == null)
        {
            Debug.Log("No connection to " + target);
            return;
        }

        int unitsToSend = origin.currentUnits;
        squadManager.InitializeSquad(origin, target, origin.ownerId, unitsToSend, speed: 1, path);

        origin.ReduceUnits(unitsToSend);
    }
}
