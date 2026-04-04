using System;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    public List<Node> allNodes = new List<Node>();

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
