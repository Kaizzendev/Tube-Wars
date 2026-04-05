using System;
using System.Collections.Generic;
using UnityEngine;

namespace Node
{
    public class NodeManager : MonoBehaviour
    {
        public List<Node> allNodes = new List<Node>();


        private void OnEnable()
        {
            SquadEventManager.onSquadReachNode += ReceiveSquad;
        }

        private void OnDisable()
        {
            SquadEventManager.onSquadReachNode -= ReceiveSquad;
        }

        private void ReceiveSquad(Node node, Squad squad)
        {
            node.ReceiveSquad(squad);
        }

        private void Start()
        {
            Node[] nodes = FindObjectsByType<Node>(FindObjectsSortMode.None);
            allNodes = new List<Node>(nodes);
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            foreach (Node node in allNodes)
            {
                node.Tick(deltaTime);
            }
        }
    }
}
