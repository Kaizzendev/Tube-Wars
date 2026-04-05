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
        [SerializeField] internal Color color;

        public event Action onChangeLeader;
        
        [Header("Conections")]
        public List<Node> neighbours;
        
        public NodeScriptable nodeData;

        private Material material;
        
        private void Awake()
        {
            LoadColor(ownerId);
        }

        private void LoadColor(int ownerId)
        {
            switch (ownerId)
            {
                case 0:
                    color = Color.grey;
                    break;
                case 1:
                    color = Color.red;
                    break;
                case 2:
                    color = Color.blue;
                    break;
            }
            
            material = GetComponent<Renderer>().material;
            material.SetColor("_BaseColor", color);
        }

        public bool IsConnectedTo(Node targetNode)
        {
            return neighbours.Contains(targetNode);
        }

        public void Tick(float deltaTime)
        {
            productionTimer += deltaTime;
            if (productionTimer >= nodeData.productionRate)
            {
                ProduceUnit();
                productionTimer = 0f;
            }
        }

        public void ProduceUnit()
        {
            currentUnits = Mathf.Min(++currentUnits, nodeData.maxUnits);
        }
        
        public void ReduceUnits(int unitsToReduce)
        {
            currentUnits -= unitsToReduce;
        }

        public void ReduceUnits(Squad squad)
        {
           currentUnits -= squad.units;
           if (currentUnits < 0)
           {
               ChangeLeader(squad, currentUnits);
           }
        }

        public void ChangeLeader(Squad squad, int expectedUnits)
        {
           ownerId = squad.ownerId;
           currentUnits = Mathf.Abs(expectedUnits);
           LoadColor(ownerId);
           onChangeLeader?.Invoke();
        }

        public void IncreaseUnits(int unitsToIncrease)
        {
            currentUnits += unitsToIncrease;
            if (currentUnits >= nodeData.maxUnits)
            {
                currentUnits = nodeData.maxUnits;
            }
        }

        public void ReceiveSquad(Squad squad)
        {
            if (squad.ownerId == ownerId)
            {
                IncreaseUnits(squad.units);
            }

            if (squad.ownerId != ownerId)
            {
                ReduceUnits(squad);
            }
            
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
