using System;
using System.Collections.Generic;
using UnityEngine;

namespace Node
{
    public class Node : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] internal int ownerId;
        [SerializeField] internal int currentUnits;
        internal Color color;
        private float _productionTimer;
        internal float _upgradeTimer;
        private Material _material;
        internal bool isUpgrading;
        [Header("Conections")]
        public List<Node> neighbours;
        
        public NodeScriptable nodeData;
        public event Action onChangeLeader;

        [HideInInspector] public Node parent;
        [HideInInspector] public bool isVisited;
        
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
            
            _material = GetComponent<Renderer>().material;
            _material.SetColor("_BaseColor", color);
        }

        private void UpgradeNode()
        {
            switch (nodeData.tier)
            {
                case 1:
                    nodeData = Resources.Load<NodeScriptable>("Node/NodeScriptableTier2");
                    break;
                case 2:
                    nodeData = Resources.Load<NodeScriptable>("Node/NodeScriptableTier3");
                    break;
            }
        }

        public void Tick(float deltaTime)
        {
            _productionTimer += deltaTime;
            if (_productionTimer >= nodeData.productionRate)
            {
                ProduceUnit();
                _productionTimer = 0f;
            }
        }

        public void ProduceUnit()
        {
            currentUnits = Mathf.Min(++currentUnits, nodeData.maxUnits);
            CheckUpgradeAvailable();
        }
        
        public void ReduceUnits(int unitsToReduce)
        {
            currentUnits -= unitsToReduce;
            PauseUpgrade();
        }

        public void ReduceUnits(Squad squad)
        {
           currentUnits -= squad.units;
           if (currentUnits < 0)
           {
               ChangeLeader(squad, currentUnits);
           }
           PauseUpgrade();
        }

        private void PauseUpgrade()
        {
            isUpgrading = false;
        }

        public void ChangeLeader(Squad squad, int expectedUnits)
        {
           ownerId = squad.ownerId;
           currentUnits = Mathf.Abs(expectedUnits);
           LoadColor(ownerId);
           onChangeLeader?.Invoke();
        }

        private void IncreaseUnits(int unitsToIncrease)
        {
            currentUnits += unitsToIncrease;
            CheckUpgradeAvailable();
        }

        private void CheckUpgradeAvailable()
        {
            if (currentUnits >= nodeData.maxUnits && nodeData.tier != 3)
            {
                currentUnits = nodeData.maxUnits;
                isUpgrading = true;
            }
        }

        private void Update()
        {
            if (!isUpgrading)
            {
                return;
            }
            UpgradeProcess();
        }

        private void UpgradeProcess()
        {
            _upgradeTimer +=  Time.deltaTime;
            if (_upgradeTimer >= nodeData.upgradeTimer)
            {
                _upgradeTimer = 0f;
                UpgradeNode();
                isUpgrading = false;
                
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
            Destroy(squad.gameObject);
            
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
