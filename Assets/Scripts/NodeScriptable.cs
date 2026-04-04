using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NodeScriptable", menuName = "Scriptable Objects/NodeScriptable")]
public class NodeScriptable : ScriptableObject
{
    [Header("Data")] 
    [SerializeField] private string nodeName;
    [SerializeField] private int productionRate;
    [SerializeField] private int ownerId;
     public int maxConnections;

}
