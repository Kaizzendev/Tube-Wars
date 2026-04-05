using System;
using System.Collections.Generic;
using UnityEngine;

namespace Node
{
    public class Node : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private int id;
        [SerializeField] internal int ownerId;
        [SerializeField] internal int currentUnits = 4;
        [SerializeField] internal float productionTimer;
        
        [Header("Conections")]
        public List<Node> neighbours;
        
        public NodeScriptable nodeData;
        
        public bool IsConnectedTo(Node targetNode)
        {
            return neighbours.Contains(targetNode);
        }

        // public void Tick(float deltaTime)
        // {
        //     productionTimer += deltaTime;
        //     if (productionTimer >= nodeData.productionRate)
        //     {
        //         ProduceUnit();
        //         productionTimer = 0f;
        //     }
        // }

        public void ProduceUnit()
        {
            currentUnits = Mathf.Min(++currentUnits, nodeData.maxUnits);
        }

        public void ReduceUnits(int unitsToReduce)
        {
           currentUnits -= unitsToReduce;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            foreach (var node in neighbours)
            {
                Gizmos.DrawLine(transform.position, node.transform.position);
            }
        }
    }
}
