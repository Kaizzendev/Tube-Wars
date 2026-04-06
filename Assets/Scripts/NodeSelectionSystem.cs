using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Node
{
    public class NodeSelectionSystem : MonoBehaviour
    {
        private Node _originNode;
        private Node _targetNode;

        public SquadManager squadManager;
        public Camera mainCamera;
        public PathFinding pathFinding;
        
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

        private void HandleNodeClick(Node clickedNode)
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
                
                SendUnits(_originNode, _targetNode);
                _originNode = null;
                _targetNode = null;
            }
            else
            {
                _originNode = null;
            }
        }

        private void SendUnits(Node origin, Node target)
        {
            List<Node> path = pathFinding.GetPath(origin, target);
            if (path == null)
            {
                Debug.Log("No connection to " + target);
                return;
            }
            Debug.Log(path.ToString());

            int unitsToSend = origin.currentUnits;
            squadManager.InitializeSquad(origin, target, origin.ownerId, unitsToSend, speed: 1, path);

            origin.ReduceUnits(unitsToSend);
        }
    }
}
