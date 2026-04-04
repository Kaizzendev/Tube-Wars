using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NodeScriptable", menuName = "Scriptable Objects/NodeScriptable")]
public class NodeScriptable : ScriptableObject
{
    [Header("Data")] 
    [SerializeField] private int tier;
    public float productionRate;
    [SerializeField] private int maxUnits;

}
