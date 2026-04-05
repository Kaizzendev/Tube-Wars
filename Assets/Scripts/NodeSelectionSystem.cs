using UnityEngine;
using UnityEngine.InputSystem;

namespace Node
{
    public class NodeSelectionSystem : MonoBehaviour
    {
        private Node originNode;
        private Node targetNode;

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
            if (originNode == null)
            {
                originNode = clickedNode;
                //TODO: Resaltar nodo seleccionado SelectedNode()
            }
            else if (clickedNode != originNode)
            {
                targetNode = clickedNode; 
                //TODO: Resaltar nodo seleccionado SelectedNode()
                
                
                SendUnits(originNode, targetNode);
                originNode = null;
                targetNode = null;
            }
            else
            {
                originNode = null;
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
