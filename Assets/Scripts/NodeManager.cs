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
}
