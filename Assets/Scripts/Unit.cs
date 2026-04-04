using UnityEngine;

public class Unit : MonoBehaviour
{
   [SerializeField] private int ownerId;
   [SerializeField] private float speed;
   [SerializeField] private Node originNode;
   [SerializeField] private Node targetNode;

   public void Initailize(Node originNode, int ownerId, float speed)
   {
      this.originNode = originNode;
      this.ownerId = ownerId;
      this.speed = speed;
      
      transform.position = originNode.transform.position;
   }
}
