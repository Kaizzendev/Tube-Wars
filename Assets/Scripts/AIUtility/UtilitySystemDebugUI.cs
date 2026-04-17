using System;
using System.Collections.Generic;
using AIUtility;
using TMPro;
using UnityEngine;

public class UtilitySystemDebugUI : MonoBehaviour
{
    private List<UtilitySystemBrain> _brains = new List<UtilitySystemBrain>();
    [SerializeField] private TMP_Text teamText;
    [SerializeField] private TMP_Text actionsText;
    UtilitySystemBrain selectedBrain = null;
    [SerializeField] private GameObject actionPrefabUI;
    private void OnEnable()
    {
        GameEventManager.onNodeSelected += LoadData;
    }

    private void OnDisable()
    {
        GameEventManager.onNodeSelected -= LoadData;
    }

    private void LoadData(Node.Node node)
    {
        Transform brainsParent = GameObject.Find("BrainParent").transform;
        if (brainsParent == null) return;
        foreach (Transform child in brainsParent)
        {
            _brains.Add(child.GetComponent<UtilitySystemBrain>());
        }
        
        for (int i = 0; i < _brains.Count; i++)
        {
            if (_brains[i].self.id == node.ownerId)
            {
                selectedBrain = _brains[i];
            }
        }
        teamText.text = $"Team: {selectedBrain.self.id}";
        LoadBrainData();
    }

    private void LoadBrainData()
    {
        if (selectedBrain == null) return;
        var actionUis = FindObjectsByType<ActionUI>(FindObjectsSortMode.None);
        foreach (var ui in actionUis)
        {
            Destroy(ui.gameObject);
        }
        foreach (var actionData in selectedBrain.actionScoreDict)
        {
            var actionUI = Instantiate(actionPrefabUI, transform);
            ActionUI actionUIComponent = actionUI.GetComponent<ActionUI>();
            actionUIComponent.Setup(actionData.Key, actionData.Value);
        }
    }
}
