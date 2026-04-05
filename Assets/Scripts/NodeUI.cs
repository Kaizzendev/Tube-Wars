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
        private Node node;

        public GameObject canvas;

        private void Awake()
        {
            node = GetComponent<Node>();
        }

        private void Start()
        {
            text.text = "";
            image.color = node.color;
        }

        private void Update()
        {
            if (Camera.main != null) canvas.transform.rotation = Camera.main.transform.rotation;
            image.fillAmount =  Mathf.Clamp01((float) node.currentUnits / node.nodeData.maxUnits);
            text.text = node.currentUnits.ToString();
        }
    }
}
