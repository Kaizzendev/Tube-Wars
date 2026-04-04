using System;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private int id;
    public int ownerId;
    public List<Unit> currentUnits;
    private float productionTimer;
    
    [Header("Conections")]
    public List<Node> neighbours;
    
    public NodeScriptable nodeData;


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
        Unit unit = UnitFactory.Instance.CreateUnit(this, ownerId, speed: 5f);
        currentUnits.Add(unit);
    }

    public void ReduceUnits(int unitsToReduce)
    {
        currentUnits.RemoveRange(1, unitsToReduce);
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
