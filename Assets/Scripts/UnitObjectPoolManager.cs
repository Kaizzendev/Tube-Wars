using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitObjectPoolManager : MonoBehaviour
{
    
    public static UnitObjectPoolManager Instance;

    public GameObject unitPrefab;
    
    public List<GameObject> allUnits;
    
    [SerializeField] private int unitsToInstantiate;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        for (int i = 0; i < unitsToInstantiate; i++)
        {
            GameObject go = Instantiate(unitPrefab, transform);
            go.SetActive(false);
            allUnits.Add(go);
        }
    }

    private Unit GenerateUnit()
    {
        GameObject go = Instantiate(unitPrefab, transform);
        go.SetActive(true);
        allUnits.Add(go);
        return go.GetComponent<Unit>();
    }

    public Unit RequestUnit(Squad squad)
    {
        for (int i = 0; i < allUnits.Count; i++)
        {
            if (!allUnits[i].activeSelf)
            {
                allUnits[i].SetActive(true);
                allUnits[i].GetComponent<Unit>().Initialize(squad);
                return allUnits[i].GetComponent<Unit>();
            }
        }

        return GenerateUnit();
    }
}
