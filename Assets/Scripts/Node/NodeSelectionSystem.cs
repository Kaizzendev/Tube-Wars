using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Node
{
    public class NodeSelectionSystem : MonoBehaviour
    {
        private Node _originNode;
        private Node _targetNode;

        public Camera mainCamera;
        private CombatSystem _combatSystem;
        public void SelectNode()
        {
            if (mainCamera == null)
            {
                return;
            }
            
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Node clickedNode = hit.collider.GetComponent<Node>();
                if (clickedNode == null)
                {
                    _originNode = null;
                }
                Debug.Log(hit.collider.gameObject.name);
                HandleNodeClick(clickedNode);
            }
        }

        public void Init(CombatSystem combatSystem)
        {
            this._combatSystem = combatSystem;
        }

        private void HandleNodeClick(Node clickedNode) // TODO: check player id to execute actions
        {
            if (_originNode == null)
            {
                _originNode = clickedNode;
                //TODO: Resaltar nodo seleccionado SelectedNode()
            }
            else if (clickedNode != _originNode)
            {
                _targetNode = clickedNode; 
                //TODO: Resaltar nodo seleccionado SelectedNode()
                
                _combatSystem.SendUnits(_originNode, _targetNode);
                _originNode = null;
                _targetNode = null;
            }
            else
            {
                _originNode = null;
            }
        }

       
    }
}
