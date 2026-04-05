using System;
using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
   public int squadId;

   public void Initialize(Squad squad)
   {
      this.squadId = squad.id;
      
      transform.position = squad.originNode.transform.position;
      Material material = gameObject.GetComponent<Renderer>().material;
      material.SetColor("_BaseColor", squad.originNode.color);
      
   }
}
