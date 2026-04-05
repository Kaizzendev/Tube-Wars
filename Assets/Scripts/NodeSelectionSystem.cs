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
                    return;
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
            if (!origin.IsConnectedTo(target))
            {
                Debug.Log("No connection to " + target);
                return;
            }

            int unitsToSend = origin.currentUnits;
            squadManager.InitializeSquad(origin, target, origin.ownerId, unitsToSend, speed: 5);

            origin.ReduceUnits(unitsToSend);
        }
    }
}
