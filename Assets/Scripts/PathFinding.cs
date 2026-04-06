using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    private List<Node.Node> visitedNodes = new List<Node.Node>();
    
    public void FindPath(Node.Node originNode, Node.Node targetNode)
    {
        ClearVisited();
        Queue<Node.Node> openSet = new Queue<Node.Node>();
        originNode.parent = null;
        originNode.isVisited = true;
        visitedNodes.Add(originNode);
        
        openSet.Enqueue(originNode);
        
        while (openSet.Count > 0)
        {
            Node.Node currentNode = openSet.Dequeue();

            foreach (Node.Node neighbour in currentNode.neighbours)
            {
                if (neighbour.isVisited) continue;
                if (neighbour == targetNode)
                {
                    neighbour.isVisited = true;
                    neighbour.parent = currentNode;
                    visitedNodes.Add(neighbour);
                    return;
                }
                if (!CanTraverse(neighbour,originNode)) continue;
                
                neighbour.isVisited = true;
                neighbour.parent = currentNode;
                visitedNodes.Add(neighbour);
                
                openSet.Enqueue(neighbour);
                
            }
        }
        
    }

    public List<Node.Node> ReturnPath(Node.Node originNode, Node.Node targetNode)
    {
        List<Node.Node> path = new List<Node.Node>();
        Node.Node currentNode = targetNode;

        if (currentNode.parent == null)
        {
            return null;
        }
        
        while (currentNode != originNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Add(originNode);
        path.Reverse();
        return path;
    }

    public List<Node.Node> GetPath(Node.Node originNode, Node.Node targetNode)
    {
        FindPath(originNode, targetNode);
        return ReturnPath(originNode, targetNode);
    }

    private bool CanTraverse(Node.Node neighbour, Node.Node originNode)
    {
        return neighbour.ownerId == originNode.ownerId;
    }

    private void ClearVisited()
    {
        foreach (Node.Node node in visitedNodes)
        {
            node.isVisited = false;
            node.parent = null;
        }
        visitedNodes.Clear();
    }
}
