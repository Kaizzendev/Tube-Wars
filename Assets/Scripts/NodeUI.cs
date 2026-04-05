using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Node
{
    public class NodeUI : MonoBehaviour
    {
        public Image image;
        public TMP_Text text;
        private Node _node;

        public GameObject canvas;

        private void OnEnable()
        {
            _node.onChangeLeader += ChangeColor;
        }

        private void OnDisable()
        {
            _node.onChangeLeader -= ChangeColor;
        }

        private void Awake()
        {
            _node = GetComponent<Node>();
        }

        private void Start()
        {
            text.text = "";
            image.color = _node.color;
        }

        private void ChangeColor()
        {
            image.color = _node.color;
        }

        private void Update()
        {
            if (Camera.main != null) canvas.transform.rotation = Camera.main.transform.rotation;
            image.fillAmount =  Mathf.Clamp01((float) _node.currentUnits / _node.nodeData.maxUnits);
            text.text = _node.currentUnits.ToString();
        }
    }
}
