using System;
using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
   public void Initialize(Squad squad)
   {
      transform.position = squad.originNode.transform.position;
      gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", squad.originNode.color);

   }
}
