using System.Collections.Generic;
using UnityEngine;

namespace Node
{
    [CreateAssetMenu(fileName = "NodeScriptable", menuName = "Scriptable Objects/NodeScriptable")]
    public class NodeScriptable : ScriptableObject
    {
        [Header("Data")] 
        [SerializeField] internal int tier;
        [SerializeField] internal float productionRate;
        [SerializeField] internal int maxUnits;

    }
}
