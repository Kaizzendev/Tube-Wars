using System;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

namespace Node
{
    public class NodeUI : MonoBehaviour
    {
        public Image unitsBar;
        public TMP_Text unitsText;
        public Image upgradeBar;
        public GameObject upgradePanel;
        private Node _node;

        public GameObject canvas;

        private void OnEnable()
        {
            GameEventManager.onChangeLeader += ChangeColor;
        }

        private void OnDisable()
        {
            GameEventManager.onChangeLeader -= ChangeColor;
        }

        private void Awake()
        {
            _node = GetComponent<Node>();
        }

        private void Start()
        {
            upgradePanel.SetActive(false);
            unitsText.text = "";
            ChangeColor(null, 0,0);
        }

        private void ChangeColor(Node node, int a, int b)
        {
            unitsBar.color = _node.color;
            upgradeBar.color = _node.color;
        }

        private void Update()
        {
            if (Camera.main != null) canvas.transform.rotation = Camera.main.transform.rotation;
            
            HandleUnitUI();
            if (_node.isUpgrading)
            {
                upgradePanel.SetActive(true);
                HandleUpgradeUI();
            }
            else
            {
                upgradePanel.SetActive(false);
            }
        }

        private void HandleUnitUI()
        {
            unitsBar.fillAmount = Mathf.Clamp01((float) _node.currentUnits / _node.nodeData.maxUnits);
            unitsText.text = _node.currentUnits.ToString();
        }

        private void HandleUpgradeUI()
        {
            upgradeBar.fillAmount = Mathf.Clamp01(_node._upgradeTimer / _node.nodeData.upgradeTimer);
        }
    }
}
