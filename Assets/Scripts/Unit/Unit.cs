using System;
using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
   public void Initialize(Squad squad)
   {
      transform.position = squad.originNode.transform.position;
      Material material = gameObject.GetComponent<Renderer>().material;
      material.SetColor("_BaseColor", squad.originNode.color);
   }
}
