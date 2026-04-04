using System;
using Unity.VisualScripting;
using UnityEngine;

public class UnitFactory: MonoBehaviour
{
    private GameObject unitPrefab;

    public UnitFactory(GameObject unitPrefab)
    {
        this.unitPrefab = unitPrefab;   
    }
    public static UnitFactory Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
    }

    public Unit CreateUnit(Node origin, int owner, float speed)
    {
        GameObject go = Instantiate(unitPrefab, origin.transform.position, Quaternion.identity);
        Unit unit = go.GetComponent<Unit>();
        unit.Initailize(origin, owner, speed);
        return unit;
    }
}
