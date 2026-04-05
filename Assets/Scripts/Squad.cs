using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squad : MonoBehaviour
{
    public Node.Node originNode;
    public Node.Node targetNode;
    public Node.Node currentNode;
    public int ownerId;
    public int units;
    public float speed;

    public List<Unit> squadUnits;
    public void Initailize(Node.Node originNode, Node.Node targetNode, int ownerId, int units, float speed)
    {
        this.originNode = originNode;
        this.targetNode = targetNode;
        this.ownerId = ownerId;
        this.units = units;
        this.speed = speed;
        
        GenerateUnits(units);
        StartCoroutine(GoTo(targetNode));
    }

    public void GenerateUnits(int unitsToSend)
    {
        for (int i = 1; i < unitsToSend; i++)
        {
            Unit unit = UnitObjectPoolManager.Instance.RequestUnit(this);
            squadUnits.Add(unit);
        }
    }

    private void Update()
    {
        foreach (var unit in squadUnits)
        {
            unit.transform.position = transform.position;
        }
    }

    public IEnumerator GoTo(Node.Node targetNode)
    {
        float t = 0f;
        while (t < speed)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(originNode.transform.position, targetNode.transform.position, t / speed);
            yield return null;
        }
      
        Debug.Log("He llegado a: " + targetNode.name);
        SquadEventManager.onSquadReachNode?.Invoke(targetNode, this);
    }
}
