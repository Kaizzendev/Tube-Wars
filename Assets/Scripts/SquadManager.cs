using System;
using System.Collections.Generic;
using UnityEngine;

public class SquadManager : MonoBehaviour
{
    public Squad squadPrefab;
    
    public List<Squad> squads = new List<Squad>();
    public void InitializeSquad(Node.Node originNode, Node.Node targetNode, int ownerId, int units, float speed)
    {
        Squad squad = Instantiate(squadPrefab,originNode.transform.position, Quaternion.identity, transform);
        squad.Initailize(originNode, targetNode, ownerId, units, speed);
        squads.Add(squad);
    }
}
