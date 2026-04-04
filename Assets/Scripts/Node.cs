using System;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
        
    [Header("Conections")]
    public List<Node> neighbours;
    
    public NodeScriptable nodeData;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (var node in neighbours)
        {
            Gizmos.DrawLine(transform.position, node.transform.position);
        }
    }
}
